using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Areas.Api.Filters;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoTabelaPrecoListagem")]
    public class EventoTabelaPrecoListagemController : ApiController {
        
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request) {
            
            var RetornoApi = new DefaultDTO();
            
            try {
                
                var ViewModel = new EventoTabelaPrecoListagemVM {
                    idEvento      = UtilRequest.getInt32("idEvento"),
                    idOrganizacao = CustomExtensions.getIdOrganizacao()
                };
                
                if (ViewModel.idEvento <= 0) { 
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("A identificação do Evento deve ser informada");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (ViewModel.idOrganizacao <= 0) { 
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("A identificação da organização deve ser informada");
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var Retorno = ViewModel.carregar();
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Preços carregados com sucesso.");
                RetornoApi.listaResultados = Retorno.listaResultados;
                // RetornoApi.carregarDadosPaginacao(Retorno);

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de consulta de tabela de preços.");
                                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                    
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}