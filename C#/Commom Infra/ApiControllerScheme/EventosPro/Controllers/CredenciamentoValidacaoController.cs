using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.EventosPro;
using BLL.Permissao;
using BLL.Services;
using DAL.Eventos;
using DAL.Permissao;
using Newtonsoft.Json.Linq;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Etiquetas.ViewModels;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [Route("Api/EventosPro/CredenciamentoValidacao")]
    public class CredenciamentoValidacaoController : ApiController {
        
        //Dependencias
        // Atributos
        private IValidadorCredenciamentoBL ValidadorCredenciamentoBL { get; }
        private EtiquetaInscritoFiller FillerEtiqueta { get; }
               
        public CredenciamentoValidacaoController(IValidadorCredenciamentoBL _ValidadorCredenciamentoBL, EtiquetaInscritoFiller _FillerEtiqueta){
            
            ValidadorCredenciamentoBL = _ValidadorCredenciamentoBL;
            FillerEtiqueta = _FillerEtiqueta;
            
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage> Post() {
            
            string jsonParam = await Request.Content.ReadAsStringAsync();
            
            if (jsonParam.isEmpty()) {

                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, "Dados não enviados!"));
                
            }
            
            var info = JObject.Parse(jsonParam);
            
            int idEvento = info["idEvento"].toInt();
            
            string tipoBusca = info["tipoBusca"].stringOrEmpty();
            
            string valorBusca = info["valorBusca"].stringOrEmpty();
            
            int idInscricao = tipoBusca == "id" ? valorBusca.toInt() : 0;
            
            string nroDocumento = tipoBusca == "documento" ? valorBusca.onlyNumber() : "";
            
            UtilRetorno RetornoValidacao = ValidadorCredenciamentoBL.validar(idEvento, idInscricao, nroDocumento);
            
            if (RetornoValidacao.flagError) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, RetornoValidacao.listaErros.FirstOrDefault()));
            }
            
            EventoInscricao OInscricao = RetornoValidacao.info.ToJsonObject<EventoInscricao>();
            
            string urlEtiqueta = String.Concat(UtilConfig.linkAbsSistema, "Etiquetas/EtiquetaInscrito/?", "idEvento=", idEvento, "&", "id=", OInscricao.id);
            
            var DadosRetorno = new {
                                       idInscricao = OInscricao.id, 
                                       OInscricao.nomeInscrito, 
                                       emailInscrito = OInscricao.emailPrincipal,
                                       documentoInscrito = UtilString.formatCPFCNPJ(OInscricao.documentoInscrito),
                                       tipoInscricao = OInscricao.TipoInscricao?.descricao ?? "Não Informado",
                                       urlEtiqueta
                                   };
            
            var ORetorno = new DefaultDTO();
            
            var listaResultados = new List<object>();
            
            listaResultados.Add(DadosRetorno);
            
            ORetorno.listaResultados = listaResultados;

            ORetorno.totalRegistros = 1;
            
            ORetorno.totalPaginas = 1;
            
            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}