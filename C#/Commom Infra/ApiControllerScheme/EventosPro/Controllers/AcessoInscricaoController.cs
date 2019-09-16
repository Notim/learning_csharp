using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.EventosPro;
using BLL.EventosPro.Services;
using BLL.Services;
using DAL.ConfiguracoesInscricoes;
using DAL.ConfiguracoesInscricoes.DTO;
using Newtonsoft.Json.Linq;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.Api.EventosPro.Models.DTO;

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [Route("Api/EventosPro/AcessoInscricao")]
    public class AcessoInscricaoController : ApiController {
        
        //Dependências
        private IValidadorAcessoInscricaoBL Validador;
        
        private IInscricaoAcessoBL OInscricaoAcessoBL;
        
        //
        public AcessoInscricaoController(IValidadorAcessoInscricaoBL _Validador,
                                         IInscricaoAcessoBL _InscricaoAcessoBL) {
            
            this.Validador = _Validador;

            this.OInscricaoAcessoBL = _InscricaoAcessoBL;
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage> Post() {
            
            string jsonParam = await Request.Content.ReadAsStringAsync();
            
            if (jsonParam.isEmpty()) {

                return Request.CreateResponse(HttpStatusCode.BadRequest, new DefaultDTO(true, "Dados não enviados!"));
                
            }
            
            var info = JObject.Parse(jsonParam);
            
            int idEvento = info["idEvento"].toInt();
            
            string login = info["login"].stringOrEmpty();
            
            string senha = info["senha"].stringOrEmpty();

            var idOrganizacao = CustomExtensions.getIdOrganizacao();
            
            // Validação inicial
            var ORetornoValidacao = this.Validador.validar(idEvento, login, idOrganizacao);
            
            if (ORetornoValidacao.flagError) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new DefaultDTO(ORetornoValidacao.flagError, ORetornoValidacao.listaErros.FirstOrDefault()));
            }

            var ORetorno = new AcessoInscricaoRetornoDTO();
            
            var ConfiguracaoInscricao = ORetornoValidacao.info.ToJsonObject<ConfiguracaoInscricao>();

            if (ConfiguracaoInscricao.flagSenhaAcesso == true && senha.isEmpty()) {

                ORetorno.flagInformarSenha = true;

                ORetorno.flagErro = false;
                
                return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
            }

            // Autenticação do inscrito
            var ORetornoLogin = this.OInscricaoAcessoBL.autenticar(idEvento, login, senha);
            
            if (ORetornoLogin.flagError) {
                return Request.CreateResponse(HttpStatusCode.OK, new DefaultDTO(ORetornoLogin.flagError, ORetornoLogin.listaErros.FirstOrDefault()));
            }
            
            var DadosInscrito = ORetornoLogin.info.ToJsonObject<DadosInscritoDTO>();
            
            var DadosRetorno = new {
                DadosInscrito.idOrganizacao,
                DadosInscrito.idInscricao,
                DadosInscrito.idPessoa,
                DadosInscrito.idInscricaoComprador,
                DadosInscrito.idTipoInscricao,
                DadosInscrito.tipoInscricao,
                DadosInscrito.idEvento,
                DadosInscrito.nomeInscrito,
                DadosInscrito.emailInscrito,
                DadosInscrito.descricaoStatusIncricao,
                DadosInscrito.flagInscricaoComprador,
                flagInscricaoQuitada = DadosInscrito.dtPagamento.HasValue || DadosInscrito.dtIsencao.HasValue,
                DadosInscrito.flagBloqueado,
                dtInscricao = DadosInscrito.dtInscricao.exibirData(), 
                idA = UtilCrypt.toBase64Encode(DadosInscrito.idInscricao),
                idAcr = UtilCrypt.SHA512(DadosInscrito.idInscricao.ToString())
            };
            
            var listaResultados = new List<object>();

            listaResultados.Add(DadosRetorno);

            ORetorno.listaResultados = listaResultados;

            ORetorno.totalRegistros = 1;
            
            ORetorno.totalPaginas = 1;
            
            return Request.CreateResponse(HttpStatusCode.OK, ORetorno);
        }
    }
}