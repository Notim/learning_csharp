using System.Collections.Generic;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {

    public class EventoTipoAtracaoConsultaVM {
        public List<EventoTipoAtracaoDTO> listaTipos { get; set; }

        public EventoTipoAtracaoConsultaVM() {
            listaTipos = new List<EventoTipoAtracaoDTO>();
        }
    }

    public class EventoTipoAtracaoDTO {
        
        public int id { get; set; }

        public string descricao { get; set; }
        
    }

}