using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Eventos;
using BLL.EventosPro;
using BLL.Services;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.EventosPro.Extensions;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoLocalizacaoListagemVM {
        
        // Atributos
        private IEventoRealizacaoConsultaBL _IEventoRealizacaoConsultaBL;
        
        private IEventoLocalBL _IEventoLocalBL;

        // Serviços
        private IEventoRealizacaoConsultaBL OEventoRealizacaoConsultaBL => _IEventoRealizacaoConsultaBL = _IEventoRealizacaoConsultaBL ?? new EventoRealizacaoConsultaBL();
        
        private IEventoLocalBL OEventoLocalBL => _IEventoLocalBL = _IEventoLocalBL ?? new EventoLocalBL();
        
        //Propriedades
        public int idEvento { get; set; }
        
        public int idOrganizacao { get; set; }

        //
        public EventoLocalizacaoListagemVM() {
            
            this.idOrganizacao = CustomExtensions.getIdOrganizacao();
        }
    
        //
        public DefaultDTO carregar() {

            var ORetorno = new DefaultDTO();

            var listaProgramacao = this.carregarProgramacao();
            var idsLocalizacoes = listaProgramacao.Select(x => x.idLocal ?? 0).Distinct().ToList();
            
            var listaLocalizacoes = this.carregarLocalizacoes(idsLocalizacoes);
            
            ORetorno = this.montarRetorno(listaLocalizacoes, listaProgramacao);
            
            return ORetorno;
        }

        //
        private List<EventoRealizacao> carregarProgramacao() {
            
            var listaProgramacao = this.OEventoRealizacaoConsultaBL.query(this.idOrganizacao)
                                       .Where(x => x.idEvento == this.idEvento)
                                       .Select(x => new {
                                           x.id,
                                           x.idLocal,
                                           x.dtRealizacao,
                                           x.horarioInicio,
                                           x.horarioFinal
                                       }).ToListJsonObject<EventoRealizacao>();

            return listaProgramacao;
        }
        
        //
        private List<EventoLocal> carregarLocalizacoes(List<int> idsLocalizacoes) {

            var query = this.OEventoLocalBL.query(this.idOrganizacao)
                            .Where(x => idsLocalizacoes.Contains(x.id) && x.flagLocalFisico == "S" && !String.IsNullOrEmpty(x.googleMaps));
            
            var listaFiltrada = query.Select(x => new {
                                    x.id,
                                    x.nome,
                                    x.googleMaps,
                                    x.flagLocalFisico,
                                    x.logradouro,
                                    x.numero,
                                    x.complemento,
                                    x.bairro,
                                    x.idCidade,
                                    Cidade = new {
                                        x.Cidade.nome,
                                        Estado = new {
                                            x.Cidade.Estado.sigla
                                        }
                                    },
                                    x.nomeCidade,
                                    x.uf
                                }).OrderBy(x => x.nome).ToListJsonObject<EventoLocal>();

            return listaFiltrada;
        }

        private DefaultDTO montarRetorno(List<EventoLocal> listaLocalizacoes, List<EventoRealizacao> listaProgramacao) {
            
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;

            var listaEventoLocalizacaoRetorno = listaLocalizacoes.Select(x => new {
                                                    x.id,
                                                    x.nome,
                                                    x.googleMaps,
                                                    endereco = x.descricaoDoLocal(),
                                                    listaProgramacao = listaProgramacao.Where(c => c.idLocal == x.id)
                                                                                       .OrderBy(c => c.dtRealizacao)
                                                                                       .ThenBy(c => c.horarioInicio)
                                                                                       .Select(c => new {
                                                                                           diaRealizacao = c.dtRealizacao.Day.ToString().PadLeft(2, '0'),
                                                                                           mesRealizacao = c.dtRealizacao.Month.ToString().PadLeft(2, '0'),
                                                                                           c.horarioInicio,
                                                                                           horarioFim = c.horarioFinal,
                                                                                       })  
                                                });

            ORetorno.listaResultados = listaEventoLocalizacaoRetorno;

            return ORetorno;
        }
    }
}