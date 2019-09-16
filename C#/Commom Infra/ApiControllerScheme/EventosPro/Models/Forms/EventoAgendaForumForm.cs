namespace WEB.Areas.Api.EventosPro.Models.DTO {

    public class EventoAgendaForumForm {
        
        public int idEvento { get; set; }
        
        public int idEventoAgenda { get; set; }
        
        public int idInscricao { get; set; }
        
        public string nomeInscrito  { get; set; }
        
        public string mensagem  { get; set; }
    }

}