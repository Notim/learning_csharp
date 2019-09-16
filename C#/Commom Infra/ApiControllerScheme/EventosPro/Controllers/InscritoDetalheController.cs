using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Services;
using DAL.ConfiguracoesInscricoes.DTO;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [Route("Api/EventosPro/InscritoResumo")]
    public class InscritoResumoController : ApiController {
        
        //Dependências
        private IEventoInscricaoConsultaBL ConsultaInscricaoBL;
        
        private IEventoInscricaoAdicionalConsultaBL ConsultaInscricaoAdicionalBL;
        
        //
        public InscritoResumoController(IEventoInscricaoConsultaBL _ConsultaInscricaoBL,
                                        IEventoInscricaoAdicionalConsultaBL _ConsultaInscricaoAdicionalBL) {
            
            this.ConsultaInscricaoBL = _ConsultaInscricaoBL;

            this.ConsultaInscricaoAdicionalBL = _ConsultaInscricaoAdicionalBL;
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id) {
            
            if (id == 0) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, "Dados não enviados!"));
            }
            
            var idOrganizacao = CustomExtensions.getIdOrganizacao();
            
            var DadosInscrito = this.ConsultaInscricaoBL.query(idOrganizacao)
                                    .Where(x => x.id == id)
                                    .Select(x => new {
                                        idInscricao = x.id,
                                        x.idOrganizacao,
                                        x.nomeInscrito,
                                        x.flagInscricaoComprador
                                    }).FirstOrDefault()
                                      .ToJsonObject<DadosInscritoDTO>();
            
            if (DadosInscrito == null) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, "O inscrito informado não foi lozalizado!"));
            }
            
            var ORetorno = new DefaultDTO();
            
            if (DadosInscrito.flagInscricaoComprador == true) {
                
                var listaQtdeInscricoes = this.ConsultaInscricaoAdicionalBL.query(idOrganizacao)
                                              .Where(x => x.idInscricaoPrincipal == DadosInscrito.idInscricao)
                                              .GroupBy(x => new { x.idInscricaoPrincipal, x.idInscricaoUtilizada })
                                              .Select(x => new {
                                                  flagUtilizado = x.Key.idInscricaoPrincipal > 0,
                                                  qtde = x.Count()
                                              }).ToList();

                DadosInscrito.qtdeInscricoesCompradas = listaQtdeInscricoes.Sum(x => x.qtde);
                
                DadosInscrito.qtdeInscricoesUtilizadas = listaQtdeInscricoes.Where(x => x.flagUtilizado).Sum(x => x.qtde);
            }
            
            var DadosRetorno = new {
                DadosInscrito.idInscricao,
                DadosInscrito.idOrganizacao,
                DadosInscrito.nomeInscrito,
                DadosInscrito.flagInscricaoComprador,
                DadosInscrito.qtdeInscricoesCompradas,
                DadosInscrito.qtdeInscricoesUtilizadas
            };

            var listaResultados = new List<object>();

            listaResultados.Add(DadosRetorno);

            ORetorno.listaResultados = listaResultados;

            ORetorno.totalRegistros = 1;
            
            ORetorno.totalPaginas = 1;
            
            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}