using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.Filters;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Controllers {
    
    [AcessoComToken, Route("Api/ProcessosAvaliacoes/InscricaoEtapaDetalhes")]
    public class InscricaoEtapaDetalhesController : ApiController {

        public readonly InscricaoEtapaDetalhesFiller Filler;
        
        public InscricaoEtapaDetalhesController(InscricaoEtapaDetalhesFiller _InscricaoEtapaDetalhesFiller) {
            Filler = _InscricaoEtapaDetalhesFiller;
        }
        
        /// <summary>
        /// GET
        /// Api que faz a Consulta de areas de conhecimento de Processos de avaliação
        /// Corpo da requisição (x-www-form-urlencoded / query string): 
        ///     idProcessoAvaliacao (int)   = obrigatório, id do processo em questão
        ///     idEtapa                     = obrigatório, id da etapa em questão
        ///     idInscricao                 = obrigatório, id da inscrição em questão
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            var RetornoApi = new DefaultDTO();
            
            var FormularioConsulta   = new InscricaoEtapaDetalhesForm {
                idOrganizacao        = CustomExtensions.getIdOrganizacao(),
                idProcessoAvaliacao  = UtilRequest.getInt32("idProcessoAvaliacao"),
                idEtapa              = UtilRequest.getInt32("idEtapa"),
                idInscricao          = UtilRequest.getInt32("idInscricao")
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
                    RetornoApi.listaMensagens.Add("A etapa deve ser informada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                if (FormularioConsulta.idInscricao <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("A inscricao deve ser iformada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var RetornoConsulta = Filler.carregar(FormularioConsulta);
                
                if (RetornoConsulta.id.toInt() <= 0) {
                    RetornoApi.flagErro = false;
                    RetornoApi.listaMensagens.Add("Nenhuma inscrição na etapa foi encontrada.");
                    RetornoApi.listaResultados = null;
                    
                    return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Inscrição na etapa encontrada com sucesso.");
                RetornoApi.listaResultados = RetornoConsulta;

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                var message = ex.getLogError("Erro no serviço de detalhes de inscrição em etapa de processos de avaliação");

                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}