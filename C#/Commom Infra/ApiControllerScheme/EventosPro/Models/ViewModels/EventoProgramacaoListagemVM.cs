using System;
using System.Collections.Generic;
using System.Linq;
using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.EventosPro.Extensions;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoProgramacaoListagemVM {
        
        // Atributos
        private IEventoAgendaConsultaBL _IEventoAgendaConsultaBL;
        private IEventoRealizacaoConsultaBL _IEventoRealizacaoConsultaBL;
        private IEventoAgendaTemaConsultaBL _IEventoAgendaTemaConsultaBL;
        private IEventoAgendaPalestranteConsultaBL _IEventoAgendaPalestranteConsultaBL;

        // Serviços
        private IEventoAgendaConsultaBL OEventoAgendaConsultaBL => _IEventoAgendaConsultaBL = _IEventoAgendaConsultaBL ?? new EventoAgendaConsultaBL();
        private IEventoRealizacaoConsultaBL OEventoRealizacaoConsultaBL => _IEventoRealizacaoConsultaBL = _IEventoRealizacaoConsultaBL ?? new EventoRealizacaoConsultaBL();
        private IEventoAgendaTemaConsultaBL OEventoAgendaTemaConsultaBL => _IEventoAgendaTemaConsultaBL = _IEventoAgendaTemaConsultaBL ?? new EventoAgendaTemaConsultaBL();
        private IEventoAgendaPalestranteConsultaBL OEventoAgendaPalestranteConsultaBL => _IEventoAgendaPalestranteConsultaBL = _IEventoAgendaPalestranteConsultaBL ?? new EventoAgendaPalestranteConsultaBL();

        //Propriedades
        public int idEvento { get; set; }
        public int idOrganizacao { get; set; }

        public EventoProgramacaoListagemVM() {
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }

        public DefaultDTO carregar() {
            var ORetorno = new DefaultDTO();
            var listaProgramacao = this.carregarProgramacao();
            ORetorno = this.montarRetorno(listaProgramacao);
            return ORetorno;
        }

        private List<EventoAgenda> carregarProgramacao() {
            
            var idsRealizacoes = this.OEventoRealizacaoConsultaBL.query(this.idOrganizacao).Where(x => x.idEvento == this.idEvento).Select(x => x.id).ToList();
           
            List<int> idsPalestrantes = UtilRequest.getListInt("idsPalestrantes");
            
            List<int> idsEventoAgenda = new List<int>();

            if (idsPalestrantes.Any()) {
                idsEventoAgenda = this.OEventoAgendaPalestranteConsultaBL.query(this.idOrganizacao)
                                       .Where(x => idsPalestrantes.Contains(x.idPalestrante) && x.idEventoAgenda > 0 && x.dtAceite != null)
                                       .Select(x => x.idEventoAgenda ?? 0).ToList();
            }
            
            var query = this.OEventoAgendaConsultaBL.query(this.idOrganizacao).Where(x => idsRealizacoes.Contains(x.idEventoRealizacao));

            bool? flagOnline = UtilRequest.getBool("flagOnline");

            if (flagOnline != null) {
                query = query.Where(x => x.flagOnline == flagOnline);
            }

            if (idsEventoAgenda.Any()) {
                query = query.Where(x => idsEventoAgenda.Contains(x.id));
            }

            var listaProgramacao = query.Select(x => new {
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

           }).OrderBy(x => x.horarioInicio).ToListJsonObject<EventoAgenda>();

            return listaProgramacao;
        }
        
        private DefaultDTO montarRetorno(List<EventoAgenda> listaEventoProgramacao) {
           
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            var listaEventoAgendaTema = OEventoAgendaTemaConsultaBL.query()
                                                  .Where(x => x.EventoAgenda.EventoRealizacao.idEvento == this.idEvento)
                                                  .Select(x => new {
                                                      x.id,
                                                      x.idEventoAgenda,
                                                      EventoTema= new
                                                      {
                                                         id = x.id,
                                                         descricao = x.EventoTema != null ? x.EventoTema.descricao : ""    
                                                      },
                                                  }).ToListJsonObject<EventoAgendaTema>() ?? new List<EventoAgendaTema>();
            
            
            var listaEventoProgramacaoRetorno = listaEventoProgramacao.Select(x => new {
                x.id,
                x.titulo,
                x.idTipoAtracao,
                tipoAtracao = x.EventoTipoAtracao.descricao,
                x.chamada,
                x.descricao,
                x.nomeEspaco,
                x.horarioInicio,
                x.horarioFim,
                x.flagOnline,
                x.idEventoRealizacao,
                x.EventoRealizacao.dtRealizacao,
                diaRealizacao = x.EventoRealizacao.dtRealizacao.Day,
                diaSemanaRealizacao =x.EventoRealizacao.dtRealizacao.ToString("ddd").ToUpper(),
                tituloEvento = x.EventoRealizacao.Evento.titulo,
                localEventoString = x.EventoRealizacao.EventoLocal.descricaoDoLocal(),
                listaTemas = listaEventoAgendaTema.Where(y => y.idEventoAgenda == x.id).Select(y => new {
                    y.EventoTema.id,
                    y.EventoTema.descricao
                })
            });
            
            ORetorno.listaResultados = listaEventoProgramacaoRetorno;
            return ORetorno;
        }
    }
}