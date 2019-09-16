using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers{
    
    [AcessoComToken, Route("Api/EventosPro/EventoPatrocinadorListagem")]
    public class EventoPatrocinadorListagemController : ApiController{

        [HttpGet]
        public HttpResponseMessage Get(int idEvento) {
            
            var ORetorno = new DefaultDTO();
            var ViewModel = new EventoPatrocinadorListagemVM();

            ViewModel.idEvento = idEvento;
            ORetorno = ViewModel.carregar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}