using System;
using System.Collections.Generic;
using System.Linq;

using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using PagedList;
using WEB.Areas.Api.EventosPro.Models.DTO;
using WEB.Areas.Api.EventosPro.Models.ViewModels;
using WEB.Extensions;
using WEB.Helpers;

namespace WEB.Areas.Api.EventosPro.Models.Fillers {

    public class EventoAgendaFavoritoConsultaFiller {
        
        private EventoAgendaFavoritoForm       Form      { get; set; }
        private EventoAgendaFavoritoConsultaVM ViewModel { get; set; }       

        private readonly IEventoAgendaFavoritoConsultaBL ConsultaEventoAgendaFavorito;
        private readonly IEventoAgendaConsultaBL         ConsultaEventoAgenda;

        public EventoAgendaFavoritoConsultaFiller(IEventoAgendaFavoritoConsultaBL _IEventoAgendaFavoritoConsultaBL, 
                                                  IEventoAgendaConsultaBL         _IEventoAgendaConsultaBL) {
            
            ConsultaEventoAgendaFavorito = _IEventoAgendaFavoritoConsultaBL;
            ConsultaEventoAgenda         = _IEventoAgendaConsultaBL;
            
            ViewModel = new EventoAgendaFavoritoConsultaVM(); 
        }
        
        public EventoAgendaFavoritoConsultaVM carregar(EventoAgendaFavoritoForm _EventoAgendaFavoritoForm) {
            
            Form = _EventoAgendaFavoritoForm;
            
            List<int> idsAtracoesFavoritas = this.carregarIdsAtracoes();
                
            if (!idsAtracoesFavoritas.Any()) {
                return ViewModel ?? new EventoAgendaFavoritoConsultaVM();
            }
            
            ViewModel.listaAtracoes = this.carregarAtracoes(idsAtracoesFavoritas);
            
            return ViewModel ?? new EventoAgendaFavoritoConsultaVM();
        }
        
        private List<int> carregarIdsAtracoes() {
            
            var query = ConsultaEventoAgendaFavorito.query(UtilRequest.getIdOrganizacao())
                                                    .Where(x => x.idEvento == Form.idEvento && x.idInscricao == Form.idInscricao);
            
            if (!Form.idDevice.isEmpty()) {
                query =  query.Where(x => x.idDevice == Form.idDevice);
            }
            
            if (Form.idEventoAgenda > 0) {
                query =  query.Where(x => x.idEventoAgenda == Form.idEventoAgenda);
            }
            
            return query.Select(x => x.idEventoAgenda).ToList();
        }
        
        private List<EventoAgenda> carregarAtracoes(List<int> idsAtracoes) {
            
            return this.ConsultaEventoAgenda.query(UtilRequest.getIdOrganizacao())
                       .Where(x => idsAtracoes.Contains(x.id))
                       .Select(x => new {                                                                                    
                            x.id,
                            x.titulo,
                            idTipoAtracao = x.idTipoAtracao ?? 0,
                            EventoTipoAtracao = new {
                                x.EventoTipoAtracao.descricao
                            },
                            x.chamada,
                            x.descricao,
                            x.horarioInicio,
                            x.horarioFim,
                            x.nomeEspaco,
                            x.flagOnline,
                            x.idEventoRealizacao,
                            EventoRealizacao = new {
                                x.EventoRealizacao.dtRealizacao,
                                Evento = new { x.EventoRealizacao.Evento.titulo },
                                    EventoLocal = new {
                                        flagLocalFisico = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.flagLocalFisico : x.EventoRealizacao.Evento.EventoLocal.flagLocalFisico,
                                        nome = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.nome : x.EventoRealizacao.Evento.EventoLocal.nome,
                                        logradouro = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.logradouro : x.EventoRealizacao.Evento.EventoLocal.logradouro,
                                        numero = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.numero : x.EventoRealizacao.Evento.EventoLocal.numero,
                                        bairro = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.bairro : x.EventoRealizacao.Evento.EventoLocal.bairro,
                                        complemento = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.complemento : x.EventoRealizacao.Evento.EventoLocal.complemento,
                                        nomeCidade = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.nomeCidade : x.EventoRealizacao.Evento.EventoLocal.nomeCidade,
                                        uf = x.EventoRealizacao.EventoLocal.id > 0 ? x.EventoRealizacao.EventoLocal.uf : x.EventoRealizacao.Evento.EventoLocal.uf,
                                }
                            }
                       }).OrderBy( x => x.EventoRealizacao.dtRealizacao).ThenBy(x => x.horarioInicio)
                       .ToListJsonObject<EventoAgenda>();
        }
    }

}