using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using BLL.EventosPro;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.EventosPro.Models.Fillers;
using WEB.Areas.Api.Filters;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoAgendaAvaliacaoExclusao")]
    public class EventoAgendaAvaliacaoExclusaoController : ApiController {
        private readonly IEventoAgendaAvaliacaoExclusaoBL ExclusaoEventoAgendaAvaliacao;

        public EventoAgendaAvaliacaoExclusaoController(IEventoAgendaAvaliacaoExclusaoBL _IEventoAgendaAvaliacaoExclusaoBL) {
            ExclusaoEventoAgendaAvaliacao = _IEventoAgendaAvaliacaoExclusaoBL;
        }                

        /// <summary>
        /// POST
        /// Api que faz a Exclusão de uma Avaliação de uma atração
        /// Corpo da requisição (x-www-form-urlencoded):
        ///     idAvaliacao = obrigatório, id da Avaliação da atração
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request) {
            var RetornoApi = new DefaultDTO();

            try {
                var idFavorito = UtilRequest.getInt32("idAvaliacao");

                if (idFavorito <= 0) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("A identificação da atração Avaliada deve ser informada");

                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }

                var RetornoExclusao = ExclusaoEventoAgendaAvaliacao.excluir(idFavorito);
                if (RetornoExclusao.flagError) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.AddRange(RetornoExclusao.listaErros);

                    return Request.CreateResponse(HttpStatusCode.BadRequest, RetornoApi);
                }

                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Avaliação da atração Removida com sucesso");

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            }
            catch (Exception ex) {
                var message = ex.getLogError($"Erro no serviço de exclusão de avaliação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}