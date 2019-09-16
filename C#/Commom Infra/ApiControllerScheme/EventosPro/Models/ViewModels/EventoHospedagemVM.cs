using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.EventosPro;
using BLL.EventosPro.Interfaces;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoHospedagemVM {
        
        // Atributos
        private IEventoHospedagemConsultaBL _IEventoHospedagemConsultaBL;

        // Serviços
        private IEventoHospedagemConsultaBL OEventoHospedagemConsultaBL => _IEventoHospedagemConsultaBL = _IEventoHospedagemConsultaBL ?? new EventoHospedagemConsultaBL();

        //Propriedades
        public int idEvento { get; set; }
        public int idOrganizacao { get; set; }

        public EventoHospedagemVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar() {

            var ORetorno = new DefaultDTO();
            
            var query = this.montarQuery();
            
            var objeto = this.filtrar(query);
             
            ORetorno = this.montarRetorno(objeto);
            
            return ORetorno;
        }

        private IQueryable<EventoHospedagem> montarQuery() {

            var query = this.OEventoHospedagemConsultaBL.query(this.idOrganizacao)
                            .Where(x => x.idEvento == this.idEvento && 
                                        x.idEventoLocal == null);

            return query;
        }

        private EventoHospedagem filtrar(IQueryable<EventoHospedagem> query) {

            var queryFiltrada = query.Select(x => new {
                                    x.id,
                                    x.idEvento,
                                    x.conteudo
                                }).FirstOrDefault().ToJsonObject<EventoHospedagem>();

            return queryFiltrada;
        }

        private DefaultDTO montarRetorno(EventoHospedagem OEventoHospedagem) {
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var OEventoHospedagemRetorno = new {
                OEventoHospedagem.id,
                OEventoHospedagem.idEvento,
                OEventoHospedagem.conteudo
            };

            ORetorno.listaResultados = OEventoHospedagemRetorno;

            return ORetorno;
        }
    }
}