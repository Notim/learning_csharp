using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Arquivos;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoPalestranteDetalheVM {
        
        // Atributos
        private IEventoPalestranteConsultaBL _IEventoPalestranteConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;
        private IArquivoUploadBL _IArquivoUploadBL;
        private IPalestranteAreaAtuacaoConsultaBL _IPalestranteAreaAtuacaoConsultaBL;

        // Serviços
        private IEventoPalestranteConsultaBL OEventoPalestranteConsultaBL => _IEventoPalestranteConsultaBL = _IEventoPalestranteConsultaBL ?? new EventoPalestranteConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();
        private IArquivoUploadBL OArquivoUploadBL => _IArquivoUploadBL = _IArquivoUploadBL ?? new ArquivoUploadBL();
        private IPalestranteAreaAtuacaoConsultaBL OPalestranteAreaAtuacaoConsultaBL => _IPalestranteAreaAtuacaoConsultaBL = _IPalestranteAreaAtuacaoConsultaBL ?? new PalestranteAreaAtuacaoConsultaBL();

        
        // Propriedades
        public int idEvento { get; set; }
        public int idPalestrante { get; set; }
        public int idOrganizacao { get; set; }

        public EventoPalestranteDetalheVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar() {
            
            var ORetorno = new DefaultDTO();
            var query = this.montarQuery();
            var eventoPalestrante = this.filtrarDetalhe(query);

            if (eventoPalestrante == null) {
                ORetorno.flagErro = true;
                ORetorno.listaMensagens.Add("Nenhum Palestrante foi encontrado.");
                
                return ORetorno;
            }

            eventoPalestrante = this.carregarArquivo(eventoPalestrante);
            ORetorno = this.montarRetorno(eventoPalestrante);

            return ORetorno;
        }
        
     
        private IQueryable<EventoPalestrante> montarQuery() {

            var query = this.OEventoPalestranteConsultaBL.query(this.idOrganizacao)
                .Where(x => x.idPalestrante == this.idPalestrante && x.idEvento == this.idEvento).Distinct();

            return query;
        }

        private EventoPalestrante filtrarDetalhe(IQueryable<EventoPalestrante> query) {

            var eventoPalestrante = query.Select(x => new {
                x.id,
                x.idPalestrante,
                x.idEvento,
                Palestrante = new
                {
                    x.Palestrante.instagram,
                    x.Palestrante.linkedin,
                    x.Palestrante.facebook,
                    x.Palestrante.twitter,
                    x.Palestrante.youtube,
                    x.Palestrante.curriculo,                    
                    Pessoa = new
                    {
                        x.Palestrante.Pessoa.nome,
                        x.Palestrante.Pessoa.nroDocumento,
                        x.Palestrante.Pessoa.profissao,
                        x.Palestrante.Pessoa.enderecoWeb,
                        x.Palestrante.Pessoa.emailPrincipal,
                        x.Palestrante.Pessoa.emailSecundario,
                        x.Palestrante.Pessoa.nroTelPrincipal,
                        x.Palestrante.Pessoa.nroTelSecundario,
                        x.Palestrante.Pessoa.nroTelTerciario,
                        x.Palestrante.Pessoa.flagEstrangeiro,
                        PaisOrigem = new
                        {
                            nome = x.Palestrante.Pessoa.PaisOrigem != null ? x.Palestrante.Pessoa.PaisOrigem.nome : "",   
                        }
                    }
                },
            }).FirstOrDefault().ToJsonObject<EventoPalestrante>();

            return eventoPalestrante;
        }

        private EventoPalestrante carregarArquivo(EventoPalestrante eventoPalestrante) {
            
            var arquivo = this.OArquivoUploadFotoBL.carregarPrincipal(eventoPalestrante.idPalestrante, EntityTypes.PALESTRANTE);
            eventoPalestrante.Palestrante.ArquivoUpload = arquivo;

            var curriculo = this.OArquivoUploadBL.carregar(eventoPalestrante.idPalestrante, "documento", EntityTypes.PALESTRANTE);
            eventoPalestrante.Palestrante.ArquivoCurriculo = curriculo;    

            return eventoPalestrante;
        }

        private DefaultDTO montarRetorno(EventoPalestrante eventoPalestrante) {
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var listaPalestranteAreaAtuacao = OPalestranteAreaAtuacaoConsultaBL.query()
                                                  .Where(x => x.idPalestrante == eventoPalestrante.idPalestrante)
                                                  .Select(x => new {
                                                      x.id,
                                                      x.idPalestrante,
                                                      x.idAreaAtuacao,
                                                      AreaAtuacao = new {
                                                          x.AreaAtuacao.descricao
                                                      }
                                                  }).ToListJsonObject<PalestranteAreaAtuacao>() ?? new List<PalestranteAreaAtuacao>();
            
            var eventoPalestranteRetorno = new {
                eventoPalestrante.id,
                eventoPalestrante.idPalestrante,
                eventoPalestrante.Palestrante.Pessoa.nome,
                eventoPalestrante.Palestrante.Pessoa.nroDocumento,
                eventoPalestrante.Palestrante.Pessoa.profissao,
                eventoPalestrante.Palestrante.instagram,
                eventoPalestrante.Palestrante.linkedin,
                eventoPalestrante.Palestrante.facebook,
                eventoPalestrante.Palestrante.twitter,
                eventoPalestrante.Palestrante.youtube,
                eventoPalestrante.Palestrante.Pessoa.enderecoWeb,
                eventoPalestrante.Palestrante.Pessoa.emailPrincipal,
                eventoPalestrante.Palestrante.Pessoa.emailSecundario,
                eventoPalestrante.Palestrante.Pessoa.nroTelPrincipal,
                eventoPalestrante.Palestrante.Pessoa.nroTelSecundario,
                eventoPalestrante.Palestrante.Pessoa.nroTelTerciario,
                eventoPalestrante.Palestrante.Pessoa.flagEstrangeiro,
                paisOrigem = eventoPalestrante.Palestrante.Pessoa.PaisOrigem.nome,
                eventoPalestrante.Palestrante.curriculo,
                urlFotoPrincipalThumb = eventoPalestrante.Palestrante.ArquivoUpload?.linkImagem("sistema") ?? "",
                urlFotoPrincipal = eventoPalestrante.Palestrante.ArquivoUpload?.linkImagem() ?? "",
                linkCurriculo = eventoPalestrante.Palestrante.ArquivoCurriculo?.linkArquivo(),
                listaAreasAtuacao = listaPalestranteAreaAtuacao.Where(y => y.idPalestrante == eventoPalestrante.idPalestrante).Select(y => new {
                    y.idAreaAtuacao,
                    y.AreaAtuacao.descricao
                })

            };

            ORetorno.listaResultados = eventoPalestranteRetorno;

            return ORetorno;
        }
    }
}