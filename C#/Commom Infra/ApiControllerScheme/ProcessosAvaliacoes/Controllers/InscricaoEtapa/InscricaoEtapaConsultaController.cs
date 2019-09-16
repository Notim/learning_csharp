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

    [AcessoComToken, Route("Api/ProcessosAvaliacoes/InscricaoEtapaConsulta")]
    public class InscricaoEtapaConsultaController : ApiController {

        public readonly InscricaoEtapaConsultaFiller Filler;
        
        public InscricaoEtapaConsultaController(InscricaoEtapaConsultaFiller _AprovadosEtapaConsultaFiller) {
            Filler = _AprovadosEtapaConsultaFiller;
        }
        
        /// <summary>
        /// GET
        /// Api que faz a Consulta de areas de conhecimento de Processos de avaliação
        /// Corpo da requisição (x-www-form-urlencoded / query string): 
        ///     idProcessoAvaliacao  (int)   = obrigatório, id do processo em questão
        ///     idEtapa              (int)   = obrigatório, id da etapa em questão
        ///     flagAprovados        (bool)  = opcional, trás somente participacoes em etapas que foram aprovadas
        ///     idsAreasConhecimento (int[]) = opcional, o filtro será feito em inscrições com areas de conhecimento específicas
        ///     nroPagina            (int)   = opcional, qtd de páginas
        ///     nroRegistros         (int)   = opcional, qtd registros por página
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            var RetornoApi = new DefaultDTO();
            
            var FormularioConsulta   = new InscricaoEtapaConsultaForm {
                idOrganizacao        = CustomExtensions.getIdOrganizacao(),
                idProcessoAvaliacao  = UtilRequest.getInt32("idProcessoAvaliacao"),
                idEtapa              = UtilRequest.getInt32("idEtapa"),
                idsAreasConhecimento = UtilRequest.getListInt("idsAreasConhecimento"),
                flagAprovados        = UtilRequest.getBool("flagAprovados"),
                nroRegistros         = UtilRequest.getNroRegistros(),
                nroPagina            = UtilRequest.getNroPagina()
            };

            try {
                if (FormularioConsulta.idProcessoAvaliacao <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("O processo de avaliação deve ser informado.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (FormularioConsulta.idEtapa <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("A Etapa deve ser informada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var RetornoConsulta = Filler.carregar(FormularioConsulta);
                
                if (!RetornoConsulta.listaAprovados.Any()) {
                    RetornoApi.flagErro = false;
                    RetornoApi.listaMensagens.Add("Nenhuma inscrição na etapa foi encontrada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Inscrições na etapa listadas com sucesso.");
                RetornoApi.listaResultados = RetornoConsulta.listaAprovados;
                RetornoApi.carregarDadosPaginacao(RetornoConsulta.listaAprovados);

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                var message = ex.getLogError("Erro no serviço de listagem de inscrição em etapas de processos de avaliação");

                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}