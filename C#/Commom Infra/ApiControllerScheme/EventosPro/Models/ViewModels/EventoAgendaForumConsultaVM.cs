using System.Collections.Generic;
using DAL.Eventos;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {

    public class EventoAgendaForumConsultaVM {
        public List<EventoAgendaForum> listaOcorrencias { get; set; }

        public EventoAgendaForumConsultaVM() {
            listaOcorrencias = new List<EventoAgendaForum>();
        }
    }

}