using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.Filters;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Controllers {

    [AcessoComToken, Route("Api/ProcessosAvaliacoes/ProcessoAvaliacaoConsulta")]
    public class ProcessoAvaliacaoConsultaController : ApiController {

        public readonly ProcessoAvaliacaoConsultaFiller Filler;
        
        public ProcessoAvaliacaoConsultaController(ProcessoAvaliacaoConsultaFiller _ProcessoAvaliacaoConsultaFiller) {
            Filler = _ProcessoAvaliacaoConsultaFiller;
        }
        
        /// <summary>
        /// GET / POST
        /// Api que faz a Consulta de Processos de avaliação
        /// Corpo da requisição (x-www-form-urlencoded / query string):
        ///     ids          ([]int) = opcional, lista de ids dos processos
        ///     valorBusca  (string) = opcional, valor de busca para achar pela descrição etc
        ///     idTipoProcesso (int) = opcional, filtrar pelo tipo de processo
        ///     nroPagina      (int) = opcional, qtd de páginas
        ///     nroRegistros   (int) = opcional, qtd registros por página
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet, HttpPost]
        public HttpResponseMessage Index(HttpRequestMessage request) {
            var RetornoApi = new DefaultDTO();

            var FormularioConsulta = new ProcessoAvaliacaoConsultaForm {
                ids = UtilRequest.getListInt("ids"),
                idOrganizacao  = CustomExtensions.getIdOrganizacao(),
                idTipoProcesso = UtilRequest.getInt32("idTipoProcesso"),
                valorBusca     = UtilRequest.getString("valorBusca"),
                nroRegistros   = UtilRequest.getNroRegistros(),
                nroPagina      = UtilRequest.getNroPagina()
            };

            try {
                var RetornoConsulta = Filler.carregar(FormularioConsulta);

                if (!RetornoConsulta.listaProcessos.Any()) {
                    RetornoApi.flagErro = false;
                    RetornoApi.listaMensagens.Add("Nenhum processo de avaliação foi encontrado.");
                    RetornoApi.listaResultados = RetornoConsulta.listaProcessos;
                    
                    return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Processos de avaliações listadas com sucesso.");
                RetornoApi.listaResultados = RetornoConsulta.listaProcessos;
                RetornoApi.carregarDadosPaginacao(RetornoConsulta.listaProcessos);

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            }
            catch (Exception ex) {
                var message = ex.getLogError($"Erro no serviço de listagem de processos de avaliação");

                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}