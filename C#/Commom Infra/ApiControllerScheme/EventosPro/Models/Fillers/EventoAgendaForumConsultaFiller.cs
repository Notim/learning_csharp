using System;
using System.Linq;
using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.EventosPro.Models.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.Fillers {

    public class EventoAgendaForumConsultaFiller {
        
        private EventoAgendaForumConsultaVM ViewModel { get; set; }

        private readonly IEventoAgendaForumConsultaBL EventoAgendaForumConsultaBL;

        public EventoAgendaForumConsultaFiller(IEventoAgendaForumConsultaBL _IEventoAgendaForumConsultaBL) {
            
            EventoAgendaForumConsultaBL = _IEventoAgendaForumConsultaBL;
            
            ViewModel = new EventoAgendaForumConsultaVM(); 
            
        }

        public EventoAgendaForumConsultaVM carregar(int idEvento, int idEventoAgenda) {
            
            this.ViewModel.listaOcorrencias = this.EventoAgendaForumConsultaBL.query(UtilRequest.getIdOrganizacao())
                                                  .Where(x => x.idEvento == idEvento && x.idEventoAgenda == idEventoAgenda)
                                                  .Select(x => new {
                                                      x.id,
                                                      x.idEvento,
                                                      x.idInscricao,
                                                      x.idEventoAgenda,
                                                      x.dtCadastro,
                                                      x.nomeInscrito,
                                                      x.mensagem
                                                  }).OrderBy( x => x.dtCadastro)
                                                  .ToListJsonObject<EventoAgendaForum>();
            
            return ViewModel ?? new EventoAgendaForumConsultaVM();
        }
        
    }

}