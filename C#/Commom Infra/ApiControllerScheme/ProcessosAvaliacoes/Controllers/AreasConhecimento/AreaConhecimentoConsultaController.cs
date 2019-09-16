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

    [AcessoComToken, Route("Api/ProcessosAvaliacoes/AreaConhecimentoConsulta")]
    public class AreaConhecimentoConsultaController : ApiController {

        public readonly AreaConhecimentoConsultaFiller Filler;
        
        public AreaConhecimentoConsultaController(AreaConhecimentoConsultaFiller _AreaConhecimentoConsultaFiller) {
            Filler = _AreaConhecimentoConsultaFiller;
        }
        
        /// <summary>
        /// GET
        /// Api que faz a Consulta de areas de conhecimento de Processos de avaliação
        /// Corpo da requisição (x-www-form-urlencoded / query string): 
        ///     idProcessoAvaliacao (int) = obrigatório, id do processo em questão
        ///     nroPagina      (int)      = opcional, qtd de páginas
        ///     nroRegistros   (int)      = opcional, qtd registros por página
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            var RetornoApi = new DefaultDTO();

            var FormularioConsulta  = new AreaConhecimentoConsultaForm {
                idOrganizacao       = CustomExtensions.getIdOrganizacao(),
                idProcessoAvaliacao = UtilRequest.getInt32("idProcessoAvaliacao"),
                nroRegistros        = UtilRequest.getNroRegistros(),
                nroPagina           = UtilRequest.getNroPagina()
            };

            try {
                if (FormularioConsulta.idProcessoAvaliacao <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("O processo de avaliação deve ser informado.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var RetornoConsulta = Filler.carregar(FormularioConsulta);
                
                if (!RetornoConsulta.listaAreasConhecimento.Any()) {
                    RetornoApi.flagErro = false;
                    RetornoApi.listaMensagens.Add("Nenhuma área de conhecimento foi encontrada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Áreas de conhecimento listadas com sucesso.");
                RetornoApi.listaResultados = RetornoConsulta.listaAreasConhecimento;
                RetornoApi.carregarDadosPaginacao(RetornoConsulta.listaAreasConhecimento);

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            }
            catch (Exception ex) {
                var message = ex.getLogError($"Erro no serviço de listagem de áreas de conhecimento de processos de avaliação");

                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}