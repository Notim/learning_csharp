using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Financeiro;
using BLL.Services;
using DAL.Eventos;
using DAL.Financeiro;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Helpers;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class HistoricoEventosVM {
        
        // Atributos
        private IEventoInscricaoConsultaBL _IEventoInscricaoConsultaBL;

        private IEventoRealizacaoConsultaBL _IEventoRealizacaoConsultaBL;
        
        private ITituloReceitaConsultaBL _ITituloReceitaConsultaBL;
        
        // Serviços
        private IEventoInscricaoConsultaBL OEventoInscricaoConsultaBL => _IEventoInscricaoConsultaBL = _IEventoInscricaoConsultaBL ?? new EventoInscricaoConsultaBL();
        
        private IEventoRealizacaoConsultaBL OEventoRealizacaoConsultaBL => _IEventoRealizacaoConsultaBL = _IEventoRealizacaoConsultaBL ?? new EventoRealizacaoConsultaBL();
        
        private ITituloReceitaConsultaBL OTituloReceitaBL => _ITituloReceitaConsultaBL = _ITituloReceitaConsultaBL ?? new TituloReceitaConsultaBL();
        
        // Propriedades
        public int id { get; set; }
        
        public int idOrganizacao { get; set; }
        
        //
        public HistoricoEventosVM(){
            
        }
       
        //
        public DefaultDTO carregar(){
            
            var ORetorno = new DefaultDTO();
            
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
            
            this.id = UtilRequest.getInt32("id");

            if (this.id == 0) {

                ORetorno.flagErro = true;
                
                ORetorno.listaMensagens.Add("O associado/não associado não foi identificado.");

                return ORetorno;
            }

            var query = this.montarQuery();
            
            var listagem = this.filtrarListagem(query);
            
            ORetorno = this.montarRetorno(listagem);
            
            return ORetorno;
        }
        
            
        private IQueryable<EventoInscricao> montarQuery() {
            
            var query = this.OEventoInscricaoConsultaBL.query(this.idOrganizacao)
                            .Where(x => x.idAssociado == this.id || x.idNaoAssociado == this.id);
            
            return query;
        }
        
        private List<EventoInscricao> filtrarListagem(IQueryable<EventoInscricao> query){

            var listaFiltrada = query.Select(x => new {
                x.id,            
                x.idEvento,
                Evento = new {
                    x.Evento.id,
                    x.Evento.titulo
                },
                x.dtInscricao,
                x.dtCadastro,
                x.dtPagamento,
                x.dtIsencao,
                x.dtCancelamento,
                x.idTipoInscricao,
                TipoInscricao = new {
                    x.TipoInscricao.descricao
                },
                x.valorInscricao,
                x.valorInscricaoOriginal, 
                x.valorDesconto,
                x.valorDescontoAssociado,
                x.valorDescontoTipoAssociado,
                x.flagGratuito,
                x.idStatusInscricao,
                StatusInscricao = new {
                    x.StatusInscricao.descricao
                }
            }).OrderBy(x => x.id).ToListJsonObject<EventoInscricao>();
            
            return listaFiltrada;
        }           

        private DefaultDTO montarRetorno(List<EventoInscricao> listaInscricoes){
            
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;

            if (!listaInscricoes.Any()) {

                return ORetorno;
            }

            var idsEventos = listaInscricoes.Select(x => x.idEvento).Distinct().ToList();
            
            var listaRealizacoesEvento = this.OEventoRealizacaoConsultaBL.query(this.idOrganizacao)
                                             .Where(x => idsEventos.Contains(x.idEvento))
                                             .Select(x => new { x.id, x.idEvento, x.dtRealizacao })
                                             .ToListJsonObject<EventoRealizacao>();
                                             
            
            var idsInscricoes = listaInscricoes.Select(x => (int?) x.id).ToList();
            
            var listaTitulosReceita = this.OTituloReceitaBL.query(this.idOrganizacao)
                                          .Where(x => x.idTipoReceita == TipoReceitaConst.INSCRICAO_EVENTO && idsInscricoes.Contains(x.idReceita))
                                          .Select(x => new { x.id, x.idReceita }).ToListJsonObject<TituloReceita>();

            var listaRetorno = listaInscricoes.Select(x => new {
                                   x.id,                
                                   tituloEvento = x.Evento?.titulo,
                                   dtInscricao = x.dtInscricao.exibirData(),
                                   dtCadastro = x.dtCadastro.exibirData(),
                                   flagCancelado = x.dtCancelamento.HasValue,
                                   flagPago = x.dtPagamento.HasValue,
                                   flagIsento = x.dtIsencao.HasValue,
                                   x.idTipoInscricao,
                                   descricaoTipoInscricao = x.TipoInscricao?.descricao,
                                   valorInscricao = x.valorInscricaoComDesconto().ToString("C"), 
                                   flagGratuito = x.flagGratuito ?? false,
                                   x.idStatusInscricao,
                                   descricaoStatusInscricao = x.StatusInscricao?.descricao,
                                   idTituloReceita = listaTitulosReceita.FirstOrDefault(c => c.idReceita == x.id)?.id.toInt(),
                                   idTituloReceitaCrypt = listaTitulosReceita.FirstOrDefault(c => c.idReceita == x.id) != null ? UtilCrypt.toBase64Encode(listaTitulosReceita.FirstOrDefault(c => c.idReceita == x.id).id) : "",
                                   flagEventoEmAndamento = listaRealizacoesEvento.Any() && listaRealizacoesEvento.Where(c => c.idEvento == x.idEvento).Min(c => c.dtRealizacao) <= DateTime.Today && listaRealizacoesEvento.Where(c => c.idEvento == x.idEvento).Max(c => c.dtRealizacao) >= DateTime.Today,
                                   flagEventoFinalizado = listaRealizacoesEvento.Any() && listaRealizacoesEvento.Where(c => c.idEvento == x.idEvento).Max(c => c.dtRealizacao) < DateTime.Today
                               });
            
            ORetorno.listaResultados = listaRetorno;
            
            return ORetorno;
        }
    }
}