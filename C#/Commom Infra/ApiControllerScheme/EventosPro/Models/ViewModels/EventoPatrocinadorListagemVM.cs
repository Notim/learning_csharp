using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using BLL.Arquivos;
using BLL.EventosPro;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using DAL.Patrocinadores;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoPatrocinadorListagemVM {
        
        // Atributos
        private IEventoPatrocinadorConsultaBL _IEventoPatrocinadorConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;

        // Serviços
        private IEventoPatrocinadorConsultaBL OEventoPatrocinadorConsultaBL => _IEventoPatrocinadorConsultaBL = _IEventoPatrocinadorConsultaBL ?? new EventoPatrocinadorConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();

        //Propriedades
        public int idEvento { get; set; }
        public int idOrganizacao { get; set; }

        public EventoPatrocinadorListagemVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar() {

            var ORetorno = new DefaultDTO();
            
            var listaPatrocinadores = this.carregarPatrocinadores();
            
            listaPatrocinadores = this.carregarArquivos(listaPatrocinadores);
            
            ORetorno = this.montarRetorno(listaPatrocinadores);
            
            return ORetorno;
        }

        private List<EventoPatrocinador> carregarPatrocinadores() {

            var query = this.OEventoPatrocinadorConsultaBL.query(this.idOrganizacao)
                            .Where(x => x.idEvento == this.idEvento);
            
            var listaFiltrada = query.Select(x => new {
                                    x.id,
                                    x.idPatrocinador,
                                    x.idPlanoPatrocinio,
                                    Patrocinador = new {
                                        Pessoa = new {
                                            x.Patrocinador.Pessoa.nome,
                                            x.Patrocinador.Pessoa.enderecoWeb
                                        }
                                    },
                                    PlanoPatrocinio = new {
                                        x.PlanoPatrocinio.descricao,
                                        x.PlanoPatrocinio.ordem
                                    }
                                }).ToListJsonObject<EventoPatrocinador>();

            return listaFiltrada;
        }

        private List<EventoPatrocinador> carregarArquivos(List<EventoPatrocinador> listaEventoPatrocinador) {

            var idsPatrocinador = listaEventoPatrocinador.Select(x => x.idPatrocinador).ToList();

            var listaArquivos = this.OArquivoUploadFotoBL.listar(0, EntityTypes.PATROCINADOR, "S")
                                    .Where(x => idsPatrocinador.Contains(x.idReferenciaEntidade)).ToList();
            
            listaEventoPatrocinador.ForEach(x => {
                x.Patrocinador.ArquivoUpload = listaArquivos.FirstOrDefault(a => a.idReferenciaEntidade == x.idPatrocinador);
            });

            return listaEventoPatrocinador;
        }

        private DefaultDTO montarRetorno(List<EventoPatrocinador> listaEventoPatrocinador) {
            
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            var listaEventoPatrocinadorRetorno = listaEventoPatrocinador.Select(x => new {
                                                    x.id,
                                                    x.idPatrocinador,
                                                    x.idPlanoPatrocinio,
                                                    planoPatrocinio = x.PlanoPatrocinio.descricao,  
                                                    ordem = x.PlanoPatrocinio.ordem,
                                                    patrocinador = x.Patrocinador.Pessoa.nome,
                                                    x.Patrocinador.Pessoa.enderecoWeb,
                                                    urlFotoPrincipal = x.Patrocinador.ArquivoUpload.linkImagem(),
                                                    urlFotoOriginal = x.Patrocinador.ArquivoUpload.linkImagem(),
                                                    urlFotoThumb = x.Patrocinador.ArquivoUpload.linkImagem("logotipo"),
                                                }).OrderBy(x => x.ordem).ToList();

            ORetorno.listaResultados = listaEventoPatrocinadorRetorno;

            return ORetorno;
        }
        
    }
    
}