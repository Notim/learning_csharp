using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.ConfiguracoesInscricoes;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Services;
using DAL.ConfiguracoesInscricoes;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoTabelaPrecoListagemVM {
        
        // Atributos Serviços
        private IEventoDescontoAntecipacaoBL           _IEventoDescontoAntecipacaoBL;
        private ITipoInscricaoTabelaPrecoConsultaBL    _ITipoInscricaoTabelaPrecoConsultaBL;
        private IEventoConfiguracaoInscricaoConsultaBL _IEventoConfiguracaoInscricaoConsultaBL;
        private IEventoTipoInscricaoConsultaBL         _IEventoTipoInscricaoConsultaBL;

        // Propriedades Serviços
        private IEventoDescontoAntecipacaoBL           OEventoDescontoAntecipacaoBL => _IEventoDescontoAntecipacaoBL = _IEventoDescontoAntecipacaoBL ?? new EventoDescontoAntecipacaoBL();
        private ITipoInscricaoTabelaPrecoConsultaBL    OTipoInscricaoTabelaPrecoConsultaBL => _ITipoInscricaoTabelaPrecoConsultaBL = _ITipoInscricaoTabelaPrecoConsultaBL ?? new EventoTipoInscricaoTabelaPrecoConsultaBL();
        private IEventoConfiguracaoInscricaoConsultaBL OEventoConfiguracaoInscricaoConsultaBL => _IEventoConfiguracaoInscricaoConsultaBL = _IEventoConfiguracaoInscricaoConsultaBL ?? new EventoConfiguracaoInscricaoConsultaBL();
        private IEventoTipoInscricaoConsultaBL         OEventoTipoInscricaoConsultaBL => _IEventoTipoInscricaoConsultaBL = _IEventoTipoInscricaoConsultaBL ?? new EventoTipoInscricaoConsultaBL();

        // Propriedades
        public int idEvento      { get; set; }
        public int idOrganizacao { get; set; }
        

        public DefaultDTO carregar() {
            
            var query    = this.montarQuery();
            var listagem = this.filtrarListagem(query);
            var ORetorno = this.montarRetorno(listagem);
            
            return ORetorno;
        }

        private IQueryable<EventoTipoInscricao> montarQuery() {

            var query = this.OEventoTipoInscricaoConsultaBL.query(this.idOrganizacao).Where(x => x.idEvento == this.idEvento);

            return query;
        }

        private List<EventoTipoInscricao> filtrarListagem(IQueryable<EventoTipoInscricao> query) {

            var listaFiltrada = query.Select(
                x => new {
                    x.id,
                    x.descricao, 
                    x.flagGratuito,
                    x.flagCompraLote,
                    x.flagPublico,
                }
            ).OrderBy(x => x.descricao)
            .ToListJsonObject<EventoTipoInscricao>();

            return listaFiltrada;
        }
        
        private List<TipoInscricaoTabelaPreco> carregarListaPrecos() {

            var listaFiltrada = OTipoInscricaoTabelaPrecoConsultaBL.query(this.idOrganizacao)
                                                                   .Where(x => x.idEvento == this.idEvento)
                                                                   .Select(
                                                                       x => new {
                                                                           x.id,
                                                                           x.idTipoInscricaoEvento,
                                                                           x.valor
                                                                       }
                                                                   ).OrderBy(x => x.valor).ToListJsonObject<TipoInscricaoTabelaPreco>();

            return listaFiltrada;
        }

        private List<EventoDescontoAntecipacao> carregarListaDescontos() {

            var listaDescontos = OEventoDescontoAntecipacaoBL.query(this.idOrganizacao)
                                                             .Where(x => x.idEvento == this.idEvento && x.dtLimite >= DateTime.Now)
                                                             .Select(
                                                                 x => new {
                                                                    x.id,
                                                                    x.idTipoInscricao,
                                                                    x.dtLimite,
                                                                    x.valorDesconto
                                                                 }
                                                             ).OrderBy(x => x.dtLimite)
                                                             .ToListJsonObject<EventoDescontoAntecipacao>();

            return listaDescontos;

        }

        private bool verificarGratuidadeEvento() {

            var flagEventoGratuito = OEventoConfiguracaoInscricaoConsultaBL.query(this.idOrganizacao)
                                                                           .Where(x => x.idEvento == this.idEvento && x.idTipoInscricao == null)
                                                                           .Select(x => x.flagGratuito)
                                                                           .FirstOrDefault() ?? false;

            return flagEventoGratuito;

        }
        
        private DefaultDTO montarRetorno(List<EventoTipoInscricao> listaEventoTipoInscricao) {

            var ORetorno = new DefaultDTO {
                flagErro = false
            };

            var listaDescontos = this.carregarListaDescontos();
            
            var listaPrecos = this.carregarListaPrecos();
            
            var flagEventoGratuito = this.verificarGratuidadeEvento();

            var listaEventoTabelaPrecoRetorno = listaEventoTipoInscricao.Select(
                                                                            x => new {
                                                                                x.id,
                                                                                valor = listaPrecos.FirstOrDefault(y => y.idTipoInscricaoEvento == x.id).valor ?? 0,
                                                                                tipoInscricao = x.descricao,
                                                                                x.flagGratuito,
                                                                                flagEventoGratuito,
                                                                                listaDescontosAntecipacao = listaDescontos.Where(y => y.idTipoInscricao == x.id),
                                                                                x.flagCompraLote,
                                                                                x.flagPublico,
                                                                            }
                                                                        );

            ORetorno.listaResultados = listaEventoTabelaPrecoRetorno;

            return ORetorno;
        }
    }
}