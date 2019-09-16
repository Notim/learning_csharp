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

namespace WEB.Areas.Api.EventosPro.Controllers {
    
    [Route("Api/EventosPro/AcessoEvento")]
    public class AcessoEventoController : ApiController {
        
        //Dependencias
        // Atributos
        private IValidadorAcessoEventoBL ValidadorAcessoEventoBL { get; }
        private IUsuarioSistemaAcessoBL UsuarioSistemaAcessoBL { get; }
        private IEventoConsultaBL EventoConsultaBL { get; }        
        
        public AcessoEventoController(IValidadorAcessoEventoBL _ValidadorAcessoEventoBL, IUsuarioSistemaAcessoBL _UsuarioSistemaAcessoBL , IEventoConsultaBL _EventoConsultaBL){
            
            ValidadorAcessoEventoBL = _ValidadorAcessoEventoBL;
            
            UsuarioSistemaAcessoBL = _UsuarioSistemaAcessoBL;
            
            EventoConsultaBL = _EventoConsultaBL;
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
                        
            UtilRetorno ValidacaoLogin = UsuarioSistemaAcessoBL.login(login, senha);            
            
            if (ValidacaoLogin.flagError) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new DefaultDTO(true, "Acesso Negado! Credenciais inválidas!"));
            }
            
            UsuarioSistema OUsuario = ValidacaoLogin.info.ToJsonObject<UsuarioSistema>(true);
            
            UtilRetorno ValidacaoEvento = ValidadorAcessoEventoBL.validar(OUsuario.id, idEvento, OUsuario.idOrganizacao.toInt());
            
            if (ValidacaoEvento.flagError) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new DefaultDTO(ValidacaoEvento.flagError, ValidacaoEvento.listaErros.FirstOrDefault()));
            }
            
            Evento OEvento = EventoConsultaBL.query(0)
                                             .Select(x => new {
                                                                  x.id, 
                                                                  x.idOrganizacao,
                                                                  Organizacao = new {
                                                                                        Pessoa = new {
                                                                                                         x.Organizacao.Pessoa.nome
                                                                                                     }
                                                                                    },
                                                                  x.titulo
                                                              })
                                             .FirstOrDefault(x => x.id == idEvento)
                                             .ToJsonObject<Evento>();
            
            if (OEvento == null) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new DefaultDTO(true, "Acesso Negado! O evento informado não pode ser acessado no momento!"));
            }            
            
            var DadosRetorno = new {
                                       idUsuario = OUsuario.id, 
                                       nomeUsuario = OUsuario.nome, 
                                       idEvento = OEvento.id, 
                                       tituloEvento = OEvento.titulo, 
                                       idOrganizacao = OEvento.idOrganizacao, 
                                       nomeOrganizacao = OEvento.Organizacao.Pessoa.nome
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