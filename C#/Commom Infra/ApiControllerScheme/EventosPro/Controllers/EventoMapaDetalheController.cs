using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.EventosPro.Interfaces;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers{
    
    [AcessoComToken, Route("Api/EventosPro/EventoMapaDetalhe")]
    public class EventoMapaDetalheController : ApiController { 

        // Dependências
        private IEventoMapaConsultaBL ConsultaBL;
        
        //
        public EventoMapaDetalheController(IEventoMapaConsultaBL _ConsultaBL) {

            this.ConsultaBL = _ConsultaBL;
        }

        //
        [HttpGet]
        public HttpResponseMessage Get(int idEvento) {

            var idOrganizacao = CustomExtensions.getIdOrganizacao();
            
            var DadosMapa = this.ConsultaBL.query(idOrganizacao)
                                .Where(x => x.idEvento == idEvento && x.ativo)
                                .OrderByDescending(x => x.id)
                                .Select(x => new {
                                    x.id,
                                    x.idEvento,
                                    x.conteudoMapa
                                }).FirstOrDefault();
            
            var ORetorno = new DefaultDTO();

            ORetorno.listaResultados = DadosMapa;
            
            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }  
    }
}