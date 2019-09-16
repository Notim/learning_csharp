using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using BLL.EventosPro;
using BLL.EventosPro.Validators;

using DAL.Eventos.Entities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using UTIL.Extensions;

using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.Filters;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [AcessoComToken, Route("Api/EventosPro/EventoAgendaAvaliacaoCadastro")]
    public class EventoAgendaAvaliacaoCadastroController : ApiController {
        
        private readonly IEventoAgendaAvaliacaoCadastroBL CadastroEventoAgendaAvaliacao;
        private readonly EventoAgendaAvaliacaoValidator   ValidatorEventoAgendaAvaliacao;
            
        public EventoAgendaAvaliacaoCadastroController(EventoAgendaAvaliacaoValidator   _EventoAgendaAvaliacaoValidator,
                                                       IEventoAgendaAvaliacaoCadastroBL _IEventoAgendaAvaliacaoCadastroBL) {
            
            CadastroEventoAgendaAvaliacao  = _IEventoAgendaAvaliacaoCadastroBL;
            ValidatorEventoAgendaAvaliacao = _EventoAgendaAvaliacaoValidator;
        }               
        
        /// <summary>
        /// POST
        /// Api que faz o cadastro de Avaliação de uma Atração
        /// Corpo da Requisição (JSON):
        ///     idEvento       = obrigatório
        ///     idEventoAgenda = obrigatório
        ///     idDevice       = obrigatório, id do aparelho
        ///     nota           = obrigatório, Nota que será dada ao EventoAgenda
        ///     idInscricao    = opcional(não está sendo usada)
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
                
                var Form = JsonConvert.DeserializeObject<EventoAgendaAvaliacaoForm>(jsonString, new IsoDateTimeConverter());
                EventoAgendaAvaliacao NewEventoAgendaAvaliacao = new EventoAgendaAvaliacao {
                    idDevice       = Form.idDevice,
                    idEvento       = Form.idEvento,
                    idEventoAgenda = Form.idEventoAgenda,
                    idInscricao    = Form.idInscricao,
                    nota           = Form.nota.toByte(),
                    idOrganizacao  = CustomExtensions.getIdOrganizacao()
                };
                
                var RetornoValidacao = ValidatorEventoAgendaAvaliacao.Validate(NewEventoAgendaAvaliacao);
                if (RetornoValidacao.retornarErros().Any()) { // Validação dos campos
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.AddRange(RetornoValidacao.retornarErros());
                    
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, RetornoApi);
                }
                
                var RetornoCadastro = CadastroEventoAgendaAvaliacao.salvar(NewEventoAgendaAvaliacao);
                if (RetornoCadastro.flagError) { // Validação de cadastro
                    RetornoApi.flagErro = true;
                    RetornoApi.listaMensagens.Add("Não foi possível registrar a avaliação da atração, tente novamente!");
                    RetornoApi.listaMensagens.AddRange(RetornoCadastro.listaErros);
                    
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
                }
                
                RetornoApi.flagErro = false;
                RetornoApi.listaMensagens.Add("Avaliação realizada com sucesso!");
                
                return this.Request.CreateResponse(HttpStatusCode.OK, RetornoApi);
                
            } catch (Exception ex) {
                
                var message = ex.getLogError($"Erro no serviço de cadastro de avaliação de programações.");
                
                RetornoApi.flagErro = true;
                RetornoApi.listaMensagens.Add(message);
                
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, RetornoApi);
            }
        }
    }
}