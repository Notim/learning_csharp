using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoEspacoListagemVM {
        
        // Atributos
        private IEventoAgendaConsultaBL _IEventoAgendaConsultaBL;
        
        private IEventoRealizacaoConsultaBL _IEventoRealizacaoConsultaBL;

        // Serviços
        private IEventoAgendaConsultaBL OEventoAgendaConsultaBL => _IEventoAgendaConsultaBL = _IEventoAgendaConsultaBL ?? new EventoAgendaConsultaBL();
        
        private IEventoRealizacaoConsultaBL OEventoRealizacaoConsultaBL => _IEventoRealizacaoConsultaBL = _IEventoRealizacaoConsultaBL ?? new EventoRealizacaoConsultaBL();

        //Propriedades
        public int idEvento { get; set; }
        
        public int idOrganizacao { get; set; }

        //
        public EventoEspacoListagemVM() {
            
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        //
        public DefaultDTO carregar() {

            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            ORetorno.listaResultados = this.carregarEspacos();
            
            return ORetorno;
        }

        //
        private List<string> carregarEspacos() {

            var idsRealizacoes = this.OEventoRealizacaoConsultaBL.query(this.idOrganizacao)
                                     .Where(x => x.idEvento == this.idEvento).Select(x => x.id).ToList();

            var query = this.OEventoAgendaConsultaBL.query(this.idOrganizacao)
                            .Where(x => idsRealizacoes.Contains(x.idEventoRealizacao));

            var listaEspacos = query.Select(x => x.nomeEspaco).Where(x => !String.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();

            return listaEspacos;
        }
        
    }
    
}