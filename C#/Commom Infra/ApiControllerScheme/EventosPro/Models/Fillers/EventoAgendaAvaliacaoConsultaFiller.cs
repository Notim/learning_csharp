using System;
using System.Linq;

using BLL.EventosPro;
using BLL.Services;

using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Extensions;

namespace WEB.Areas.Api.EventosPro.Models.Fillers {

    public class EventoAgendaAvaliacaoConsultaFiller {
        
        private EventoAgendaAvaliacaoForm       Form      { get; set; }
        private EventoAgendaAvaliacaoConsultaVM ViewModel { get; set; }

        private readonly IEventoAgendaAvaliacaoConsultaBL ConsultaEventoAgendaAvaliacao;
        
        public EventoAgendaAvaliacaoConsultaFiller(IEventoAgendaAvaliacaoConsultaBL _IEventoAgendaAvaliacaoConsultaBL) {
            
            ConsultaEventoAgendaAvaliacao = _IEventoAgendaAvaliacaoConsultaBL;
            
            ViewModel = new EventoAgendaAvaliacaoConsultaVM(); 
        }
        
        public EventoAgendaAvaliacaoConsultaVM carregar(EventoAgendaAvaliacaoForm _EventoAgendaAvaliacaoForm) {
            
            Form = _EventoAgendaAvaliacaoForm;
            
            carregarAtracoes();
            
            return ViewModel ?? new EventoAgendaAvaliacaoConsultaVM();
        }
        
        internal void carregarAtracoes() {
            
            this.ViewModel.listaAtracoes = ConsultaEventoAgendaAvaliacao.query(UtilRequest.getIdOrganizacao())
                                                                        .Where(x => x.idDevice == Form.idDevice && x.idEvento == Form.idEvento)
                                                                        .Select(
                                                                            x => new {
                                                                                id             = x.id,
                                                                                nota           = x.nota,
                                                                                idEventoAgenda = x.idEventoAgenda,
                                                                                titulo         = x.EventoAgenda.titulo,
                                                                                nomeEspaco     = x.EventoAgenda.nomeEspaco,
                                                                                horarioInicio  = x.EventoAgenda.horarioInicio,
                                                                                horarioFim     = x.EventoAgenda.horarioFim
                                                                            }
                                                                        ).OrderBy( x => x.horarioInicio)
                                                                        .ToPagedListJsonObject<EventoAgendaAvaliacaoDTO>(UtilRequest.getNroPagina(), UtilRequest.getNroRegistros());
        }
    }

}