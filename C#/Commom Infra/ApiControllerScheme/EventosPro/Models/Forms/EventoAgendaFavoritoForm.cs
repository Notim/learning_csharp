using Newtonsoft.Json;

namespace WEB.Areas.Api.EventosPro.Models.DTO {

    public class EventoAgendaFavoritoForm {
        
        public int idEvento { get; set; }
        
        public int idEventoAgenda { get; set; }
        
        public int? idInscricao { get; set; }
        
        public string idDevice { get; set; }
    }

}