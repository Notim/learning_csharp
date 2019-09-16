using System;
using System.Collections.Generic;
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
    
    public class EventoExpositorListagemVM {
        
        // Atributos
        private IEventoExpositorConsultaBL _IEventoExpositorConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;
        
        // Serviços
        private IEventoExpositorConsultaBL OEventoExpositorConsultaBL => _IEventoExpositorConsultaBL = _IEventoExpositorConsultaBL ?? new EventoExpositorConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();

        // Propriedades
        public int idEvento{ get; set; }
        public int idOrganizacao{ get; set; }
        
        public EventoExpositorListagemVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }
        
        public DefaultDTO carregar(){
            
            var ORetorno = new DefaultDTO();
            
            var listaPatrocinadores = this.carregarExpositores();
            
            listaPatrocinadores = this.carregarArquivos(listaPatrocinadores);
            
            ORetorno = this.montarRetorno(listaPatrocinadores);
            
            return ORetorno;
        }
        
        private List<EventoExpositor> carregarExpositores() {
            
            var query = this.OEventoExpositorConsultaBL.query(this.idOrganizacao).Where(x => x.idEvento == idEvento);
            
            var listaFiltrada = query.Select(x => new {
                                    x.id,
                                    x.idExpositor,
                                    Expositor = new {
                                        x.Expositor.nroEstande,
                                        Pessoa = new {
                                            x.Expositor.Pessoa.nome,
                                            x.Expositor.Pessoa.emailPrincipal,
                                            x.Expositor.Pessoa.enderecoWeb,
                                            x.Expositor.Pessoa.observacoes
                                        }
                                    }
                                }).OrderBy(x => x.id).ToListJsonObject<EventoExpositor>();

            return listaFiltrada;
        }

        private List<EventoExpositor> carregarArquivos(List<EventoExpositor> listaEventoExpositor){

            var idsExpositor = listaEventoExpositor.Select(x => x.idExpositor).ToList();
            
            var listaArquivos = this.OArquivoUploadFotoBL.listar(0, EntityTypes.EXPOSITOR, "S")
                                    .Where(x => idsExpositor.Contains(x.idReferenciaEntidade)).ToList();
            
            listaEventoExpositor.ForEach(x => {
                x.Expositor.ArquivoUpload = listaArquivos.FirstOrDefault(a => a.idReferenciaEntidade == x.idExpositor);
            });
            
            return listaEventoExpositor;
        }
        
        private DefaultDTO montarRetorno(List<EventoExpositor> listaEventoExpositor){
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;
            
            var listaEventoRetorno = listaEventoExpositor.Select(x => new{
                                         x.id,
                                         x.idExpositor,
                                         expositor = x.Expositor.Pessoa.nome,
                                         site = x.Expositor.Pessoa.enderecoWeb,
                                         email = x.Expositor.Pessoa.emailPrincipal,
                                         observacoes = x.Expositor.Pessoa.observacoes,
                                         nroEstande = x.Expositor.nroEstande,
                                         urlFotoPrincipalThumb = x.Expositor.ArquivoUpload?.linkImagem("sistema") ?? "",
                                         urlFotoPrincipal = x.Expositor.ArquivoUpload.linkImagem(),
                                         urlFotoOriginal = x.Expositor.ArquivoUpload.linkImagem(),
                                         urlFotoThumb = x.Expositor.ArquivoUpload.linkImagem("logotipo"),
                                     });

            ORetorno.listaResultados = listaEventoRetorno;
            
            return ORetorno;
        }
    }
}