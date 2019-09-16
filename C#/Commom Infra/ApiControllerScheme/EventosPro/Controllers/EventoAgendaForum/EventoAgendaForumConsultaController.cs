using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UTIL.Extensions;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.Fillers;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoAgendaForumConsulta")]
    public class EventoAgendaForumConsultaController : ApiController {
    
        private readonly EventoAgendaForumConsultaFiller OFiller;
        
        public EventoAgendaForumConsultaController(EventoAgendaForumConsultaFiller _Filler) {
            OFiller = _Filler;
        }              
        
        /// <summary>
        /// GET
        /// Api que faz A Consulta de mensagens de um forum de uma atração
        /// Parametros:
        ///     idEvento       = obrigatório
        ///     idEventoAgenda = obrigatório
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {

                var idEvento = UtilRequest.getInt32("idEvento");
                var idEventoAgenda = UtilRequest.getInt32("idEventoAgenda");
                
                if (idEvento.toInt() == 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível identificar o evento.");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (idEventoAgenda.toInt() == 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível identificar a atração.");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var Retorno = OFiller.carregar(idEvento, idEventoAgenda);
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Mensagens listadas com sucesso.");
                RetornoApi.listaResultados = Retorno.listaOcorrencias
                                                    .Select(x => new {
                                                        x.id,
                                                        x.idEvento,
                                                        x.idEventoAgenda,
                                                        x.idInscricao,
                                                        dtCadastro = x.dtCadastro.exibirData(true),
                                                        x.nomeInscrito,
                                                        x.mensagem
                                                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de consulta de mensagens do forum.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
        
    }

}