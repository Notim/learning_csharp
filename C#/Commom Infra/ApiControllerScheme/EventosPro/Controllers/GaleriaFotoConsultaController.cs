using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/GaleriaFotoConsulta")]
    public class GaleriaFotoConsultaController : ApiController {
        
        public HttpResponseMessage Get() {
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var ViewModel = new GaleriaFotoConsultaVM();
            ORetorno = ViewModel.carregar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);

        }

    }

}
