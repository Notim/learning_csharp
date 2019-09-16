using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DAL.Eventos;
using RestSharp.Extensions;
using WEB.Areas.Api.Default.ViewModels;
using WEB.Areas.EventosPro.Extensions;

namespace WEB.Areas.Api.EventosPro.Models.ViewModels {
    
    public class EventoProgramacaoDetalheVM {
        
        public List<EventoAgendaTema> listaEventoAgendaTema { get; set; }
        public List<EventoAgendaPalestrante> listaPalestrantes { get; set; }
        public EventoAgenda OEventoProgramacao { get; set; }
        
        public DefaultDTO montarRetorno() {
           
            var ORetorno = new DefaultDTO();
            
            ORetorno.flagErro = false;
            
            var OEventoProgramacaoRetorno = new {
                OEventoProgramacao.id,
                OEventoProgramacao.titulo,
                OEventoProgramacao.idTipoAtracao,
                tipoAtracao = OEventoProgramacao.EventoTipoAtracao.descricao,
                OEventoProgramacao.chamada,
                OEventoProgramacao.descricao,
                OEventoProgramacao.nomeEspaco,
                OEventoProgramacao.horarioInicio,
                OEventoProgramacao.horarioFim,
                OEventoProgramacao.flagOnline,
                OEventoProgramacao.idEventoRealizacao,
                OEventoProgramacao.EventoRealizacao.dtRealizacao,
                dtRealizacaoFormatado = OEventoProgramacao.EventoRealizacao.dtRealizacao.ToString(@"dddd, dd \de MMMM \de yyyy").toUppercaseWords(),
                diaRealizacao = OEventoProgramacao.EventoRealizacao.dtRealizacao.Day,
                diaSemanaRealizacao =OEventoProgramacao.EventoRealizacao.dtRealizacao.ToString("ddd").ToUpper(),
                tituloEvento = OEventoProgramacao.EventoRealizacao.Evento.titulo,
                localEventoString = OEventoProgramacao.EventoRealizacao.EventoLocal.descricaoDoLocal(),
                listaTemas = listaEventoAgendaTema
                    .Select(y => new {
                        y.EventoTema.id,
                        y.EventoTema.descricao
                    }),
                listaPalestrantes = listaPalestrantes
                    .Select(y => new {
                        id = y.idPalestrante,
                        palestrante = y.Palestrante.Pessoa.nome,
                        email = y.Palestrante.Pessoa.emailPrincipal,
                        nacionalidade = y.Palestrante.Pessoa.PaisOrigem.nome
                    })
            };
            
            ORetorno.listaResultados = OEventoProgramacaoRetorno;
            return ORetorno;
        }
    }
}