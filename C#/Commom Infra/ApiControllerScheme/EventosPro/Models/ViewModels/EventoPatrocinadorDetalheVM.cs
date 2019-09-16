using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Arquivos;
using BLL.EventosPro;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoPatrocinadorDetalheVM {
        
        // Atributos
        private IEventoPatrocinadorConsultaBL _IEventoPatrocinadorConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;

        // Serviços
        private IEventoPatrocinadorConsultaBL OEventoPatrocinadorConsultaBL => _IEventoPatrocinadorConsultaBL = _IEventoPatrocinadorConsultaBL ?? new EventoPatrocinadorConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();
        
        // Propriedades
        public int id { get; set; }
        public int idOrganizacao { get; set; }

        public EventoPatrocinadorDetalheVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar() {
            
            var ORetorno = new DefaultDTO();
            var query = this.montarQuery();
            var eventoPatrocinador = this.filtrarDetalhe(query);

            if (eventoPatrocinador == null) {
                ORetorno.flagErro = true;
                ORetorno.listaMensagens.Add("Nenhum patrocinador foi encontrado.");
                
                return ORetorno;
            }

            eventoPatrocinador = this.carregarArquivo(eventoPatrocinador);
            ORetorno = this.montarRetorno(eventoPatrocinador);

            return ORetorno;
        }
        
     

        private IQueryable<EventoPatrocinador> montarQuery() {

            var query = this.OEventoPatrocinadorConsultaBL.query(this.idOrganizacao)
                .Where(x => x.id == this.id);

            return query;
        }

        private EventoPatrocinador filtrarDetalhe(IQueryable<EventoPatrocinador> query) {

            var eventoPatrocinador = query.Select(x => new {
                x.id,
                x.idPatrocinador,
                x.idPlanoPatrocinio,
                Patrocinador = new {
                    x.Patrocinador.descricao,
                    Pessoa = new {
                        x.Patrocinador.Pessoa.nome,
                        x.Patrocinador.Pessoa.enderecoWeb,
                        x.Patrocinador.Pessoa.emailPrincipal,
                    }
                },
                PlanoPatrocinio = new {
                    x.PlanoPatrocinio.descricao
                }
            }).FirstOrDefault().ToJsonObject<EventoPatrocinador>();

            return eventoPatrocinador;
        }

        private EventoPatrocinador carregarArquivo(EventoPatrocinador eventoPatrocinador) {
            
            var arquivo = this.OArquivoUploadFotoBL.carregarPrincipal(eventoPatrocinador.idPatrocinador, EntityTypes.PATROCINADOR);
            eventoPatrocinador.Patrocinador.ArquivoUpload = arquivo;

            return eventoPatrocinador;
        }

        private DefaultDTO montarRetorno(EventoPatrocinador eventoPatrocinador) {
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var eventoPatrocinadorRetorno = new {
                eventoPatrocinador.id,
                eventoPatrocinador.idPatrocinador,
                eventoPatrocinador.idPlanoPatrocinio,
                patrocinador = eventoPatrocinador.Patrocinador.Pessoa.nome,
                eventoPatrocinador.Patrocinador.descricao,
                eventoPatrocinador.Patrocinador.Pessoa.enderecoWeb,
                eventoPatrocinador.Patrocinador.Pessoa.emailPrincipal,
                planoPatrocinio = eventoPatrocinador.PlanoPatrocinio.descricao,
                urlFotoPrincipalThumb = eventoPatrocinador.Patrocinador.ArquivoUpload?.linkImagem("sistema") ?? "",
                urlFotoPrincipal = eventoPatrocinador.Patrocinador.ArquivoUpload?.linkImagem() ?? ""
            };

            ORetorno.listaResultados = eventoPatrocinadorRetorno;

            return ORetorno;
        }
    }
}