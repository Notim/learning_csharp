using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers{
    
    [AcessoComToken, Route("Api/EventosPro/HistoricoEventosDocumento")]
    public class HistoricoEventosDocumentoController : ApiController{

        [HttpGet]
        public HttpResponseMessage Get() {
            
            var ViewModel = new HistoricoEventosDocumentoVM();

            var ORetorno = ViewModel.buscar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
        
    }
    
}