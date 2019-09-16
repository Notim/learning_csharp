using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using BLL.EventosPro;
using BLL.EventosPro.Validators;
using BLL.Services;

using DAL.Eventos.Entities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.Filters;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [AcessoComToken, Route("Api/EventosPro/EventoAgendaFavoritoCadastro")]
    public class EventoAgendaFavoritoCadastroController : ApiController {
        
        private readonly IEventoAgendaFavoritoCadastroBL CadastroEventoAgendaFavorito;
        private readonly EventoAgendaFavoritoValidator   ValidatorEventoAgendaFavorito;
            
        public EventoAgendaFavoritoCadastroController(EventoAgendaFavoritoValidator   _EventoAgendaFavoritoValidator,
                                                      IEventoAgendaFavoritoCadastroBL _IEventoAgendaFavoritoCadastroBL) {
            
            CadastroEventoAgendaFavorito  = _IEventoAgendaFavoritoCadastroBL;
            ValidatorEventoAgendaFavorito = _EventoAgendaFavoritoValidator;
        }
                       
        /// <summary>
        /// POST
        /// Api que faz o Cadastro de uma favoritação de uma atração
        /// Corpo da requisição (JSON):
        ///     idEvento       = obrigatório
        ///     idEventoAgenda = obrigatório, id da atração
        ///     idInscricao    = opcional(não está sendo usada)
        ///     idDevice       = obrigatório, id do aparelho
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
                EventoAgendaFavorito NewEventoAgendaFavorito = new EventoAgendaFavorito {
                    idDevice       = Form.idDevice,
                    idEvento       = Form.idEvento,
                    idEventoAgenda = Form.idEventoAgenda,
                    idInscricao    = Form.idInscricao,
                    idOrganizacao  = UtilRequest.getIdOrganizacao()
                };
                
                var RetornoValidacao = ValidatorEventoAgendaFavorito.Validate(NewEventoAgendaFavorito);
                if (RetornoValidacao.retornarErros().Any()) { // Validação dos campos obrigatórios
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.AddRange(RetornoValidacao.retornarErros());
                    
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var RetornoCadastro = CadastroEventoAgendaFavorito.salvar(NewEventoAgendaFavorito);
                if (RetornoCadastro.flagError) { // Validação de cadastro
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível favoritar a programação, tente novamente!");
                    RetornoApi.listaMensagens.AddRange(RetornoCadastro.listaErros);
                    
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Programação favoritada com sucesso!");
                
                return this.Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de cadastro de favoritação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                    
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }
}