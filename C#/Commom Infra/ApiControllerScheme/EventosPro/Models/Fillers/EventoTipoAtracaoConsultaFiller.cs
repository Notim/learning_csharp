using System;
using System.Linq;
using BLL.EventosPro.Interfaces;
using BLL.Services;
using WEB.Areas.Api.EventosPro.Models.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.Fillers {

    public class EventoTipoAtracaoConsultaFiller {
        
        private EventoTipoAtracaoConsultaVM ViewModel { get; set; }

        private readonly IEventoTipoAtracaoConsultaBL EventoTipoAtracaoConsultaBL;
        
        public EventoTipoAtracaoConsultaFiller(IEventoTipoAtracaoConsultaBL _IEventoTipoAtracaoConsultaBL) {
            
            EventoTipoAtracaoConsultaBL = _IEventoTipoAtracaoConsultaBL;
            
            ViewModel = new EventoTipoAtracaoConsultaVM(); 
        }
        
        public EventoTipoAtracaoConsultaVM carregar() {
            
            this.carregarTipos();
            
            return ViewModel ?? new EventoTipoAtracaoConsultaVM();
        }

        private void carregarTipos() {
            
            this.ViewModel.listaTipos = this.EventoTipoAtracaoConsultaBL.query(UtilRequest.getIdOrganizacao())
                                            .Select(x => new {
                                                        x.id,
                                                        x.descricao,
                                                    }).OrderBy( x => x.descricao)
                                            .ToListJsonObject<EventoTipoAtracaoDTO>();
        }
    }

}