using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoPalestranteListagem")]
    public class EventoPalestranteListagemController : ApiController {

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int idEvento, string idPaisOrigem = "", string valorBusca = "", bool flagAtracoes = false) {

            var ViewModel = new EventoPalestranteListagemVM {
                idEvento      = idEvento,
                idPaisOrigem  = idPaisOrigem,
                valorBusca    = valorBusca,
                flagAtracoes  = flagAtracoes,
                idOrganizacao = CustomExtensions.getIdOrganizacao()
            };

            var ORetorno = ViewModel.carregar();

            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }

}