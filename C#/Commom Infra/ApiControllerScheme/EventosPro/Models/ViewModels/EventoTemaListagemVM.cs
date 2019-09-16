using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Arquivos;
using BLL.Eventos;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;


namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoTemaListagemVM {
        
        // Atributos
        private IEventoTemaConsultaBL _IEventoTemaConsultaBL;        
        
        // Serviços
        private IEventoTemaConsultaBL OEventoTemaConsultaBL => _IEventoTemaConsultaBL = _IEventoTemaConsultaBL ?? new EventoTemaConsultaBL();        
        
        // Propriedades
        public int idEvento{ get; set; }
        public int idOrganizacao{ get; set; }
        
        public EventoTemaListagemVM(){
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }
        
        public DefaultDTO carregar(){
            
            var ORetorno = new DefaultDTO();
            var query = this.montarQuery();
            var listagem = this.filtrarListagem(query);            
            ORetorno = this.montarRetorno(listagem);
            
            return ORetorno;
        }
        
            
        private IQueryable<EventoTema> montarQuery(){
            
            var query = this.OEventoTemaConsultaBL.query(this.idOrganizacao).Where(x => x.idEvento == idEvento);
            return query;
        }
        
        private List<EventoTema> filtrarListagem(IQueryable<EventoTema> query){

            var listaFiltrada = query.Select(x => new {
                x.id,                
                x.descricao,                
            }).OrderBy(x => x.id).ToListJsonObject<EventoTema>();
            
            return listaFiltrada;
        }           

        private DefaultDTO montarRetorno(List<EventoTema> listaEventoTema){
            
            var ORetorno = new DefaultDTO();
            ORetorno.flagErro = false;

            var listaEventoRetorno = listaEventoTema.Select(x => new{
                x.id,
                x.descricao,                
            });
            
            ORetorno.listaResultados = listaEventoRetorno;
            
            return ORetorno;
        }
    }
}