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

    [AcessoComToken, Route("Api/EventosPro/EventoAgendaFavoritoConsulta")]
    public class EventoAgendaFavoritoConsultaController : ApiController {
    
        private readonly EventoAgendaFavoritoConsultaFiller FillerEventoAgendaFavoritoConsulta;
        
        public EventoAgendaFavoritoConsultaController(EventoAgendaFavoritoConsultaFiller _EventoAgendaFavoritoConsultaFiller) {
            FillerEventoAgendaFavoritoConsulta = _EventoAgendaFavoritoConsultaFiller;
        }              
        
        /// <summary>
        /// GET
        /// Api que faz A Consulta de favoritações de uma atração
        /// Parametros:
        ///     idEvento       = obrigatório
        ///     idEventoAgenda = opcional(não está sendo usada)
        ///     idInscricao    = opcional(não está sendo usada)
        ///     idDevice       = obrigatório, id do aparelho
        ///     nroPagina     = opcional, qtd de páginas
        ///     nroRegistros  = opcional, qtd registros por página
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {
                var Form = new EventoAgendaFavoritoForm {
                    idEvento       = UtilRequest.getInt32("idEvento"),
                    idEventoAgenda = UtilRequest.getInt32("idEventoAgenda"),
                    idInscricao    = UtilRequest.getInt32("idInscricao"),
                    idDevice       = UtilRequest.getString("idDevice")
                };
                
                if (Form.idEvento.toInt() == 0) {
                    
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Informe o evento.");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (Form.idInscricao.toInt() == 0) {
                    
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível identificar a inscrição.");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var Retorno = FillerEventoAgendaFavoritoConsulta.carregar(Form);

                RetornoApi = Retorno.montarRetorno();

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de consulta de favoritação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
        
    }

}