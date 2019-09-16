using System;
using System.Collections.Generic;

using PagedList;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {

    public class EventoAgendaAvaliacaoConsultaVM {
        public IPagedList<EventoAgendaAvaliacaoDTO> listaAtracoes { get; set; }

        public EventoAgendaAvaliacaoConsultaVM() {
            listaAtracoes = new List<EventoAgendaAvaliacaoDTO>().ToPagedList(1, 20);
        }
    }

    public class EventoAgendaAvaliacaoDTO {
        public string titulo { get; set; }

        public string nomeEspaco { get; set; }

        public DateTime? horarioInicio { get; set; }

        public DateTime? horarioFim { get; set; }

        public byte nota { get; set; }
    }

}