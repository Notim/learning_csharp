using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers{
    
    [AcessoComToken, Route("Api/EventosPro/EventoPalestranteDetalhe")]
    public class EventoPalestranteDetalheController : ApiController{

        [HttpGet]
        public HttpResponseMessage Get(int idPalestrante, int idEvento) {
            
            var ORetorno = new DefaultDTO();
            var ViewModel = new EventoPalestranteDetalheVM();

            ViewModel.idPalestrante = idPalestrante;
            ViewModel.idEvento = idEvento;
            ORetorno = ViewModel.carregar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}