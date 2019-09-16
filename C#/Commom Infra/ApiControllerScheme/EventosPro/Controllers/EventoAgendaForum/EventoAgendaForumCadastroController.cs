using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.EventosPro;
using BLL.EventosPro.Validators;
using DAL.Eventos;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UTIL.Extensions;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.Filters;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [AcessoComToken, Route("Api/EventosPro/EventoAgendaForumCadastro")]
    public class EventoAgendaForumCadastroController : ApiController {
        
        private readonly IEventoAgendaForumCadastroBL OEventoAgendaForumCadastroBL;
        private readonly EventoAgendaForumValidator OValidator;
        
        public EventoAgendaForumCadastroController(EventoAgendaForumValidator _EventoAgendaForumValidator,
                                                   IEventoAgendaForumCadastroBL _IEventoAgendaForumCadastroBL) {
            
            OEventoAgendaForumCadastroBL = _IEventoAgendaForumCadastroBL;
            OValidator = _EventoAgendaForumValidator;
        }
        
        /// <summary>
        /// POST
        /// Api que faz o Cadastro de uma mensagem do forum de uma atração
        /// Corpo da requisição (JSON):
        ///     idEvento = obrigatório
        ///     idEventoAgenda = obrigatório, id da atração
        ///     idInscricao = obrigatório
        ///     nomeInscrito = opcional
        ///     mensagem = obrigatório
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

                var Form = JsonConvert.DeserializeObject<EventoAgendaForumForm>(jsonString, new IsoDateTimeConverter());
                EventoAgendaForum OEventoAgendaForum = new EventoAgendaForum {
                    idEvento = Form.idEvento,
                    idEventoAgenda = Form.idEventoAgenda,
                    idInscricao = Form.idInscricao,
                    nomeInscrito = Form.nomeInscrito,
                    mensagem = Form.mensagem,
                    idOrganizacao = UtilRequest.getIdOrganizacao()
                };
                
                var RetornoValidacao = this.OValidator.Validate(OEventoAgendaForum);
                if (RetornoValidacao.retornarErros().Any()) { // Validação dos campos obrigatórios
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.AddRange(RetornoValidacao.retornarErros());
                    
                    return this.Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                }
                
                var flagSucessoCadastro = this.OEventoAgendaForumCadastroBL.salvar(OEventoAgendaForum);
                if (!flagSucessoCadastro) { // Validação de cadastro
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível salvar a mensagem, tente novamente!");
                    
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Mensagem enviada com sucesso!");
                
                return this.Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                var message = ex.getLogError($"Erro no serviço Cadastro de mensagens do forum.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                    
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }
}