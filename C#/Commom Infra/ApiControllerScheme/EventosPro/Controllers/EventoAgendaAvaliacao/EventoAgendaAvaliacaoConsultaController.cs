using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.EventosPro.Models.Fillers;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoAgendaAvaliacaoConsulta")]
    public class EventoAgendaAvaliacaoConsultaController : ApiController {
    
        private readonly EventoAgendaAvaliacaoConsultaFiller FillerEventoAgendaAvaliacaoConsulta;
        
        public EventoAgendaAvaliacaoConsultaController(EventoAgendaAvaliacaoConsultaFiller _EventoAgendaAvaliacaoConsultaFiller) {
            FillerEventoAgendaAvaliacaoConsulta = _EventoAgendaAvaliacaoConsultaFiller;
        }                        
        
        /// <summary>
        /// GET
        /// Api que faz o consulta de Avaliações de atrações
        /// Parâmetros (Query String):
        ///     idEvento       = obrigatório
        ///     idDevice       = obrigatório, id do aparelho
        ///     idInscricao    = opcional(não está sendo usada)
        ///     nroPagina     = opcional, qtd de páginas
        ///     nroRegistros  = opcional, qtd registros por página
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {
                var Form = new EventoAgendaAvaliacaoForm {
                    idDevice       = UtilRequest.getString("idDevice"),
                    idEvento       = UtilRequest.getInt32("idEvento"),
                    idInscricao    = UtilRequest.getInt32("idInscricao")
                };
                
                if (Form.idDevice.isEmpty()) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Informe o dispositivo!");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (Form.idEvento <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Informe o evento!");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var Retorno = FillerEventoAgendaAvaliacaoConsulta.carregar(Form);
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Avaliações listadas com sucesso.");
                RetornoApi.listaResultados = Retorno;
                RetornoApi.carregarDadosPaginacao(Retorno.listaAtracoes);

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            
            } catch (Exception ex) {

                var message = ex.getLogError("Erro no serviço de consulta de avaliação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                
            }
        }
    }

}