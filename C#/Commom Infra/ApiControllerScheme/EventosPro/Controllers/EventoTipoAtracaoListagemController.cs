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
    
    [AcessoComToken, Route("Api/EventosPro/EventoTipoAtracaoListagem")]
    public class EventoTipoAtracaoListagemController : ApiController {

        private EventoTipoAtracaoConsultaFiller OFiller { get; }
        
        public EventoTipoAtracaoListagemController(EventoTipoAtracaoConsultaFiller _Filler) {
            OFiller = _Filler;
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {
                
                var Retorno = OFiller.carregar();
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Tipos de atração listadas com sucesso.");
                RetornoApi.listaResultados = Retorno.listaTipos;
                
                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de listagem de tipos de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                
            }
        }
        
    }
}