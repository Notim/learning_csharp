using Newtonsoft.Json;

namespace WEB.Areas.Api.EventosPro.Models.DTO {

    public class EventoAgendaAvaliacaoForm {
        
        public int idEvento { get; set; }

        public int idEventoAgenda { get; set; }

        public int? idInscricao { get; set; }

        public string idDevice { get; set; }

        public byte? nota { get; set; }
    }

}