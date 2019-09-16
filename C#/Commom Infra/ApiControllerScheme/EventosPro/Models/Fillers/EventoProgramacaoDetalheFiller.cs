using System;
using System.Linq;
using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.EventosPro.Models.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.Fillers {

    public class EventoProgramacaoDetalheFiller {
        
        private EventoProgramacaoDetalheVM ViewModel { get; set; }

        private readonly IEventoAgendaConsultaBL EventoAgendaConsultaBL;
        private readonly IEventoAgendaTemaConsultaBL EventoAgendaTemaConsultaBL;
        private readonly IEventoAgendaPalestranteConsultaBL EventoAgendaPalestranteConsultaBL;
        
        public EventoProgramacaoDetalheFiller(IEventoAgendaConsultaBL _IEventoAgendaConsultaBL,
                                              IEventoAgendaTemaConsultaBL _IEventoAgendaTemaConsultaBL,
                                              IEventoAgendaPalestranteConsultaBL _IEventoAgendaPalestranteConsultaBL) {
            
            EventoAgendaConsultaBL = _IEventoAgendaConsultaBL;
            EventoAgendaTemaConsultaBL = _IEventoAgendaTemaConsultaBL;
            EventoAgendaPalestranteConsultaBL = _IEventoAgendaPalestranteConsultaBL;
            
            ViewModel = new EventoProgramacaoDetalheVM(); 
        }
        
        public EventoProgramacaoDetalheVM carregar(int idAtracao) {
            
            this.carregarAtracao(idAtracao);
            this.carregarTemas();
            this.carregarPalestrantes();
            
            return ViewModel ?? new EventoProgramacaoDetalheVM();
        }

        private void carregarAtracao(int idAtracao) {
            
            this.ViewModel.OEventoProgramacao = this.EventoAgendaConsultaBL.query(UtilRequest.getIdOrganizacao())
                                                    .Where(x => x.id == idAtracao)
                                                    .Select(x => new {
                                                                x.id,
                                                                x.titulo,
                                                                x.idTipoAtracao,
                                                                EventoTipoAtracao = new {
                                                                    x.EventoTipoAtracao.descricao
                                                                },
                                                                x.chamada,
                                                                x.descricao,
                                                                x.nomeEspaco,
                                                                x.horarioInicio,
                                                                x.horarioFim,
                                                                x.flagOnline,
                                                                x.idEventoRealizacao,
                                                                EventoRealizacao = new {
                                                                    x.EventoRealizacao.dtRealizacao,
                                                                    Evento = new {
                                                                        x.EventoRealizacao.Evento.titulo
                                                                    },
                                                                    x.EventoRealizacao.EventoLocal
                                                                }
                                                            }).FirstOrDefault()
                                                    .ToJsonObject<EventoAgenda>() ?? new EventoAgenda();
            
        }

        private void carregarTemas() {
            
            this.ViewModel.listaEventoAgendaTema = this.EventoAgendaTemaConsultaBL.query(UtilRequest.getIdOrganizacao())
                                                       .Where(x => x.idEventoAgenda == ViewModel.OEventoProgramacao.id)
                                                       .Select(x => new {
                                                                   x.id,
                                                                   EventoTema = new {
                                                                       id = x.EventoTema == null ? x.EventoTema.id : 0,
                                                                       x.EventoTema.descricao
                                                                   }
                                                               })
                                                       .ToListJsonObject<EventoAgendaTema>();
        }

        private void carregarPalestrantes() {
            
            this.ViewModel.listaPalestrantes = this.EventoAgendaPalestranteConsultaBL.query(UtilRequest.getIdOrganizacao())
                                                   .Where(x => x.idEventoAgenda == ViewModel.OEventoProgramacao.id && x.dtAceite != null)
                                                   .Select(x => new {
                                                               x.id,
                                                               x.idPalestrante,
                                                               Palestrante = new {
                                                                   Pessoa = new {
                                                                       x.Palestrante.Pessoa.nome,
                                                                       x.Palestrante.Pessoa.emailPrincipal,
                                                                       PaisOrigem = new {
                                                                           x.Palestrante.Pessoa.PaisOrigem.nome
                                                                       }
                                                                   }
                                                               }
                                                           })
                                                   .ToListJsonObject<EventoAgendaPalestrante>();
        }
    }

}