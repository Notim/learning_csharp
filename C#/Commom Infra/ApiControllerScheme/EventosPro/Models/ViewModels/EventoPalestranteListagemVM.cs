using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BLL.Arquivos;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Palestrantes;
using BLL.Services;

using DAL.Arquivos;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using DAL.Palestrantes;

using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoPalestranteListagemVM {
        
        // Propriedades
        public int    idEvento      { get; set; }
        public string idPaisOrigem  { get; set; }
        public string valorBusca    { get; set; }
        public int    idOrganizacao { get; set; }
        public bool   flagAtracoes  { get; set; }
        
        private IEventoAgendaPalestranteConsultaBL _IEventoAgendaPalestranteConsultaBL;
        private IPalestranteAreaAtuacaoConsultaBL _IPalestranteAreaAtuacaoConsultaBL;
        private IPalestranteConsultaBL _IPalestranteConsultaBL;
        private IArquivoUploadFotoBL _IArquivoUploadFotoBL;
        private IArquivoUploadBL _IArquivoUploadBL;
        
        // Dependencias
        private IEventoAgendaPalestranteConsultaBL OEventoAgendaPalestranteConsultaBL => _IEventoAgendaPalestranteConsultaBL = _IEventoAgendaPalestranteConsultaBL ?? new EventoAgendaPalestranteConsultaBL();
        private IPalestranteAreaAtuacaoConsultaBL OPalestranteAreaAtuacaoConsultaBL   => _IPalestranteAreaAtuacaoConsultaBL = _IPalestranteAreaAtuacaoConsultaBL ?? new PalestranteAreaAtuacaoConsultaBL();
        private IPalestranteConsultaBL OPalestranteConsultaBL                         => _IPalestranteConsultaBL = _IPalestranteConsultaBL ?? new PalestranteConsultaBL();
        private IArquivoUploadFotoBL OArquivoUploadFotoBL                             => _IArquivoUploadFotoBL = _IArquivoUploadFotoBL ?? new ArquivoUploadFotoBL();
        private IArquivoUploadBL OArquivoUploadBL                                     => _IArquivoUploadBL = _IArquivoUploadBL?? new ArquivoUploadBL();

        public DefaultDTO carregar() {
            var query    = this.montarQuery();
            var listagem = this.filtrarListagem(query);
                listagem = this.carregarArquivos(listagem);
                
            var ORetorno = this.montarRetorno(listagem);

            return ORetorno;
        }
        
        protected IQueryable<Palestrante> montarQuery() {

            var idsPalestrantes = OEventoAgendaPalestranteConsultaBL.query(this.idOrganizacao)
                                                                    .Where(x => x.idEvento == this.idEvento && x.dtAceite != null)
                                                                    .Select(x => x.idPalestrante)
                                                                    .Distinct()
                                                                    .ToList();
            
            var query = OPalestranteConsultaBL.query(idOrganizacao)
                                              .Where(x => idsPalestrantes.Contains(x.id));

            if (!idPaisOrigem.isEmpty()) {
                query = query.Where(x => x.Pessoa.idPaisOrigem == this.idPaisOrigem);
            }

            if (!valorBusca.isEmpty()) {
                query = query.Where(x => x.Pessoa.nome.Contains(valorBusca));
            }

            return query;
        }

        protected List<Palestrante> filtrarListagem(IQueryable<Palestrante> query) {
            
            var listaFiltrada = query.Select(
                x => new {
                    x.id,
                    x.instagram,
                    x.linkedin,
                    x.facebook,
                    x.twitter,
                    x.youtube,
                    Pessoa = new {
                        x.Pessoa.nome,
                        x.Pessoa.nroDocumento,
                        x.Pessoa.profissao,
                        x.Pessoa.enderecoWeb,
                        x.Pessoa.emailPrincipal,
                        x.Pessoa.emailSecundario,
                        x.Pessoa.nroTelPrincipal,
                        x.Pessoa.nroTelSecundario,
                        x.Pessoa.nroTelTerciario,
                        x.Pessoa.flagEstrangeiro,
                        x.Pessoa.idPaisOrigem,
                        PaisOrigem = new {
                            x.Pessoa.PaisOrigem.nome
                        }
                    }
                }
            ).OrderBy(x => x.Pessoa.nome)
            .ToListJsonObject<Palestrante>();

            return listaFiltrada;
        }

        protected List<Palestrante> carregarArquivos(List<Palestrante> listaEventoPalestrante) {

            var idsPalestrante = listaEventoPalestrante.Select(x => x.id).ToList();

            var listaArquivos = OArquivoUploadFotoBL.listar(0, EntityTypes.PALESTRANTE, "S")
                                                    .Where(x => idsPalestrante.Contains(x.idReferenciaEntidade))
                                                    .ToList();

            var listaCurriculo = OArquivoUploadBL.listar(0, EntityTypes.PALESTRANTE, "documento", "S")
                                                 .Where(x => idsPalestrante.Contains(x.idReferenciaEntidade))
                                                 .ToList();

            listaEventoPalestrante.ForEach(
                x => {
                    x.ArquivoUpload    = listaArquivos.FirstOrDefault(a => a.idReferenciaEntidade  == x.id) ?? new ArquivoUpload();
                    x.ArquivoCurriculo = listaCurriculo.FirstOrDefault(a => a.idReferenciaEntidade == x.id) ?? new ArquivoUpload();
                }
            );

            return listaEventoPalestrante;
        }

        protected DefaultDTO montarRetorno(List<Palestrante> listaEventoPalestrante) {

            var ORetorno = new DefaultDTO {
                flagErro = false
            };

            var idsPalestrantes = listaEventoPalestrante.Select(x => x.id).ToList();

            var listaPalestranteAreaAtuacao = OPalestranteAreaAtuacaoConsultaBL.query()
                                                                               .Where(x => idsPalestrantes.Contains(x.idPalestrante ?? 0))
                                                                               .Select(
                                                                                   x => new {
                                                                                       x.id,
                                                                                       x.idPalestrante,
                                                                                       x.idAreaAtuacao,
                                                                                       AreaAtuacao = new {
                                                                                           x.AreaAtuacao.descricao
                                                                                       }
                                                                                   }
                                                                               ).ToListJsonObject<PalestranteAreaAtuacao>() 
                                                                               ?? new List<PalestranteAreaAtuacao>();

            var listaPalestranteAtracoes = new List<EventoAgendaPalestrante>();
            if (flagAtracoes) {
                listaPalestranteAtracoes = OEventoAgendaPalestranteConsultaBL.query()
                                                                             .Where(x => idsPalestrantes.Contains(x.idPalestrante))
                                                                             .Select(
                                                                                x => new {
                                                                                    x.id,
                                                                                    x.dtAceite,
                                                                                    x.idPalestrante,
                                                                                    x.idEventoAgenda
                                                                                }
                                                                             ).ToListJsonObject<EventoAgendaPalestrante>() ?? new List<EventoAgendaPalestrante>();
            }
            
            var listaEventoPalestranteRetorno = listaEventoPalestrante.Select(
                                                                           palestrante => new {
                                                                               palestrante.id,
                                                                               palestrante = palestrante.Pessoa.nome,
                                                                               palestrante.Pessoa.nroDocumento,
                                                                               palestrante.Pessoa.profissao,
                                                                               palestrante.Pessoa.enderecoWeb,
                                                                               palestrante.Pessoa.emailPrincipal,
                                                                               palestrante.Pessoa.emailSecundario,
                                                                               palestrante.Pessoa.nroTelPrincipal,
                                                                               palestrante.Pessoa.nroTelSecundario,
                                                                               palestrante.Pessoa.nroTelTerciario,
                                                                               palestrante.Pessoa.flagEstrangeiro,
                                                                               palestrante.Pessoa.idPaisOrigem,
                                                                               palestrante.instagram,
                                                                               palestrante.linkedin,
                                                                               palestrante.facebook,
                                                                               palestrante.twitter,
                                                                               palestrante.youtube,
                                                                               nacionalidade   = palestrante.Pessoa.PaisOrigem.nome,
                                                                               tituloImagem    = palestrante.ArquivoUpload.titulo,
                                                                               legendaImagem   = palestrante.ArquivoUpload.legenda,
                                                                               linkImagem      = palestrante.ArquivoUpload.linkImagem(),
                                                                               urlFotoOriginal = palestrante.ArquivoUpload.linkImagem(),
                                                                               urlFotoThumb    = palestrante.ArquivoUpload.linkImagem("sistema"),
                                                                               linkCurriculo   = palestrante.ArquivoCurriculo.linkArquivo(),
                                                                               listaAreasAtuacao = listaPalestranteAreaAtuacao.Where(atuacao => atuacao.idPalestrante == atuacao.id)
                                                                                                                              .Select(
                                                                                                                                  atuacao => new {
                                                                                                                                      atuacao.idAreaAtuacao,
                                                                                                                                      atuacao.AreaAtuacao.descricao
                                                                                                                                  }
                                                                                                                              ),
                                                                               listaAtracoes = listaPalestranteAtracoes.Where(atracao => palestrante.id == atracao.idPalestrante && atracao.dtAceite.HasValue)
                                                                                                                       .Select(atracao => atracao.idEventoAgenda)
                                                                           }
                                                                       );

            ORetorno.listaResultados = listaEventoPalestranteRetorno;

            return ORetorno;
        }
    }

}