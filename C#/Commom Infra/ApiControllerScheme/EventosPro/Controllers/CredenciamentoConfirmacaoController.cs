using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.EventosPro;
using BLL.EventosPro.Interfaces;
using Newtonsoft.Json.Linq;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [Route("Api/EventosPro/CredenciamentoConfirmacao")]
    public class CredenciamentoConfirmacaoController : ApiController {
        
        //Dependencias
        // Atributos
        private IValidadorCredenciamentoBL ValidadorCredenciamentoBL { get; }
        private IEventoPresencaCadastroBL EventoPresencaCadastroBL { get; }
        private IEventoCredenciamentoBL EventoCredenciamentoBL { get; }
               
        public CredenciamentoConfirmacaoController(IValidadorCredenciamentoBL _ValidadorCredenciamentoBL, 
                                                   IEventoPresencaCadastroBL _EventoPresencaCadastroBL,
                                                   IEventoCredenciamentoBL _EventoCredenciamentoBL){
            
            ValidadorCredenciamentoBL = _ValidadorCredenciamentoBL;
            EventoPresencaCadastroBL = _EventoPresencaCadastroBL;
            EventoCredenciamentoBL = _EventoCredenciamentoBL;
            
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage> Post() {
            
            string jsonParam = await Request.Content.ReadAsStringAsync();
            
            if (jsonParam.isEmpty()) {

                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, "Dados não enviados!"));
                
            }
            
            var info = JObject.Parse(jsonParam);
            
            int idEvento = info["idEvento"].toInt();
            
            int idInscricao = info["idInscricao"].toInt();
            
            int idUsuario = info["idUsuario"].toInt();
            
            UtilRetorno RetornoValidacao = ValidadorCredenciamentoBL.validar(idEvento, idInscricao, "");
            
            if (RetornoValidacao.flagError) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, RetornoValidacao.listaErros.FirstOrDefault()));
            }

            var ORetorno = this.EventoCredenciamentoBL.credenciar(idEvento, idInscricao, idUsuario);

            if (ORetorno.flagError){
                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, ORetorno.listaErros.FirstOrDefault()));
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, new DefaultDTO(false, ORetorno.listaErros.FirstOrDefault()));
        }
    }
}