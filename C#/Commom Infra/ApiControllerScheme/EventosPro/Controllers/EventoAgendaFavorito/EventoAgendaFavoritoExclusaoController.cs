using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using BLL.EventosPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.EventosPro.Models.Fillers;
using WEB.Areas.Api.Filters;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Controllers {

    [AcessoComToken, Route("Api/EventosPro/EventoAgendaFavoritoExclusao")]
    public class EventoAgendaFavoritoExclusaoController : ApiController {
        private readonly IEventoAgendaFavoritoExclusaoBL ExclusaoEventoAgendaFavorito;

        public EventoAgendaFavoritoExclusaoController(IEventoAgendaFavoritoExclusaoBL _IEventoAgendaFavoritoExclusaoBL) {
            ExclusaoEventoAgendaFavorito = _IEventoAgendaFavoritoExclusaoBL;
        }      

        /// <summary>
        /// POST
        /// Api que faz a Exclusão de uma favoritação de uma atração
        /// Corpo da requisição (JSON):
        ///     idEventoAgenda = obrigatório
        ///     idInscricao = obrigatório
        ///     idEvento = não utilizado
        ///     idDevice = não utilizado
        /// </summary>
        /// <param name="request">Requisição HTTP formatada</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request) {
            
            var jsonString = await request.Content.ReadAsStringAsync();
            
            var RetornoApi = new DefaultDTO();

            try {
                
                if (!jsonString.isValidJson()) { // Validação de estrutura incorreta do json
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível receber os dados enviados, verifique o formato.");
                    
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var Form = JsonConvert.DeserializeObject<EventoAgendaFavoritoForm>(jsonString, new IsoDateTimeConverter());
                
                if (Form.idEventoAgenda <= 0) {
                    
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível identificar a programação!");

                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }

                if (Form.idInscricao <= 0) {
                    
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível identificar a inscrição!");

                    return Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }

                var RetornoExclusao = ExclusaoEventoAgendaFavorito.excluir(Form.idEventoAgenda, Form.idInscricao.toInt());

                if (RetornoExclusao.flagError) {
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.AddRange(RetornoExclusao.listaErros);

                    return Request.CreateResponse(HttpStatusCode.BadRequest, RetornoApi);
                }

                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Programação removida dos favoritos!");

                return Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
            }
            catch (Exception ex) {
                var message = ex.getLogError($"Erro no serviço de Exclusão de uma favoritação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }

}