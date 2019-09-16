using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UTIL.Extensions;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.Fillers;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [AcessoComToken, Route("Api/EventosPro/EventoProgramacaoDetalhe")]
    public class EventoProgramacaoDetalheController : ApiController {

        private EventoProgramacaoDetalheFiller OFiller { get; }
        
        public EventoProgramacaoDetalheController(EventoProgramacaoDetalheFiller _Filler) {
            OFiller = _Filler;
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {

                var idAtracao = UtilRequest.getInt32("idAtracao");
                
                var Retorno = OFiller.carregar(idAtracao);

                if (Retorno.OEventoProgramacao.id == 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível encontrar a programação informada.");
                    
                    return Request.CreateResponse(HttpStatusCode.BadRequest, RetornoApi);
                }

                RetornoApi = Retorno.montarRetorno();
                
                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de Consulta de detalhes de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                
            }
        }
        
    }
}