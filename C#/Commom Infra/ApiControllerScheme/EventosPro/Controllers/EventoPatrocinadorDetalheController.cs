using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers{
    
    [AcessoComToken, Route("Api/EventosPro/EventoPatrocinadorDetalhe")]
    public class EventoPatrocinadorDetalheController : ApiController{

        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage Get(int id) {
            
            var ORetorno = new DefaultDTO();
            var ViewModel = new EventoPatrocinadorDetalheVM();

            ViewModel.id = id;
            ORetorno = ViewModel.carregar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}