using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Arquivos;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Financeiro;
using BLL.Services;
using DAL.Arquivos.Extensions;
using DAL.Entities;
using DAL.Eventos;
using DAL.Financeiro;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.AreaAssociados;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class HistoricoEventosDocumentoVM {
        
        // Atributos
        private IInscricaoPreenchimentoBL _InscricaoPreenchimentoBL;
        
        
        // Serviços
        private IInscricaoPreenchimentoBL OInscricaoPreenchimentoBL => _InscricaoPreenchimentoBL = _InscricaoPreenchimentoBL ?? new InscricaoPreenchimentoBL();
                
        
        // Propriedades
        
        public int idOrganizacao { get; set; }
        
        public string nroDocumento { get; set; }
                
        public string nroPassaporte { get; set; }
        
        public int limite { get; set; }
        
        //
        public HistoricoEventosDocumentoVM(){
            
        }
        
        //
        public DefaultDTO buscar(){
                        
            var ORetorno = new DefaultDTO();
            
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
            
            this.nroDocumento = UtilRequest.getString("nroDocumento");                        
            this.nroPassaporte = UtilRequest.getString("nroPassaporte");
            this.limite = UtilRequest.getInt32("limite");
                        
            if (this.nroDocumento.isEmpty() && this.nroPassaporte.isEmpty()) {

                ORetorno.flagErro = true;
                
                ORetorno.listaMensagens.Add("Informe o documento para localização da inscrição.");

                return ORetorno;
            }
                        
            var listagem = this.carregarDados();
            
            ORetorno = this.montarRetorno(listagem);
            
            return ORetorno;
        }
        
        private List<EventoInscricao> carregarDados(){
            
            var listaFiltrada = this.OInscricaoPreenchimentoBL.buscarInscricoes(this.nroDocumento, this.nroPassaporte, this.idOrganizacao);
            
            this.limite = this.limite > 0 ? this.limite : 20;
            
            return listaFiltrada.Take(this.limite).ToList();
            
        }
        
        private DefaultDTO montarRetorno(List<EventoInscricao> listaInscricoes){
            
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            if (!listaInscricoes.Any()) {

                return ORetorno;
            }                                                                                                                                             
            
            ORetorno.listaResultados = listaInscricoes;
            
            return ORetorno;
        }
    }
}