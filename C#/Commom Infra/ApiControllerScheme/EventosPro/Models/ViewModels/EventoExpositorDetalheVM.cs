using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Arquivos;
using BLL.Eventos;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoExpositorDetalheVM {
        
        // Atributos
        private IEventoExpositorConsultaBL _IEventoExpositorConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;

        // Serviços
        private IEventoExpositorConsultaBL OEventoExpositorConsultaBL => _IEventoExpositorConsultaBL = _IEventoExpositorConsultaBL ?? new EventoExpositorConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();
        
        // Propriedades
        public int id { get; set; }
        public int idOrganizacao{ get; set; }

        public EventoExpositorDetalheVM(){
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar(){
            
            var ORetorno = new DefaultDTO();
            var query = this.montarQuery();
            var eventoExpositor = this.filtrarDetalhe(query);
            
            if (eventoExpositor == null) {
                ORetorno.flagErro = true;
                ORetorno.listaMensagens.Add("Nenhum expositor foi encontrado.");
                
                return ORetorno;
            }
            
            eventoExpositor = this.carregarArquivo(eventoExpositor);
            ORetorno = this.montarRetorno(eventoExpositor);

            return ORetorno;
        }
        
        private IQueryable<EventoExpositor> montarQuery(){

            var query = this.OEventoExpositorConsultaBL.query(this.idOrganizacao).Where(x => x.id == this.id);

            return query;
        }

        private EventoExpositor filtrarDetalhe(IQueryable<EventoExpositor> query){

            var eventoExpositor = query.Select(x => new{
                x.id,
                x.idExpositor,
                Expositor = new {
                    x.Expositor.nroEstande,
                    Pessoa = new {
                        x.Expositor.Pessoa.nome,
                        x.Expositor.Pessoa.enderecoWeb,
                        x.Expositor.Pessoa.emailPrincipal,
                        x.Expositor.Pessoa.observacoes
                    }
                }
            }).FirstOrDefault().ToJsonObject<EventoExpositor>();

            return eventoExpositor;
        }

        private EventoExpositor carregarArquivo(EventoExpositor eventoExpositor){
            
            var arquivo = OArquivoUploadFotoBL.carregarPrincipal(eventoExpositor.idExpositor, EntityTypes.EXPOSITOR);
            eventoExpositor.Expositor.ArquivoUpload = arquivo;

            return eventoExpositor;
        }

        private DefaultDTO montarRetorno(EventoExpositor eventoExpositor){
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var eventoExpositorRetorno = new{
                eventoExpositor.id,
                eventoExpositor.idExpositor,
                expositor = eventoExpositor.Expositor.Pessoa.nome,
                eventoExpositor.Expositor.Pessoa.enderecoWeb,
                nroEstande = eventoExpositor.Expositor.nroEstande,
                observacoes =eventoExpositor.Expositor.Pessoa.observacoes,
                eventoExpositor.Expositor.Pessoa.emailPrincipal,
                urlFotoPrincipalThumb = eventoExpositor.Expositor.ArquivoUpload?.linkImagem("sistema") ?? "",
                urlFotoPrincipal = eventoExpositor.Expositor.ArquivoUpload?.linkImagem() ?? ""
            };

            ORetorno.listaResultados = eventoExpositorRetorno;

            return ORetorno;
        }  
    }
}

