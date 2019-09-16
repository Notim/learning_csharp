using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Eventos;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.EventosPro.Extensions;
using WEB.Helpers;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {

    public class EventoAgendaFavoritoConsultaVM {
        
        public List<EventoAgenda> listaAtracoes { get; set; }

        public EventoAgendaFavoritoConsultaVM() {
            listaAtracoes = new List<EventoAgenda>();
        }
        
        public DefaultDTO montarRetorno() {
           
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            var listaEventoProgramacaoRetorno = this.listaAtracoes.Select(x => new {
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
                dtRealizacaoFormatado = x.EventoRealizacao.dtRealizacao.ToString(@"dddd, dd \de MMMM \de yyyy").toUppercaseWords(),
                diaRealizacao = x.EventoRealizacao.dtRealizacao.Day,
                diaSemanaRealizacao =x.EventoRealizacao.dtRealizacao.ToString("ddd").ToUpper(),
                tituloEvento = x.EventoRealizacao.Evento.titulo,
                localEventoString = x.EventoRealizacao.EventoLocal.descricaoDoLocal(),
            });
            
            ORetorno.listaResultados = listaEventoProgramacaoRetorno;
            return ORetorno;
        }
        
    }

}