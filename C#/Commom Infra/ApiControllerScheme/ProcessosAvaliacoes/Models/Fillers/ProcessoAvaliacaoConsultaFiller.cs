using System;
using System.Collections.Generic;
using System.Linq;

using BLL.ProcessosAvaliacoes;
using BLL.Services;

using DAL.ProcessosAvaliacoes;


using PagedList;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels;
using WEB.Extensions;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers {

    public class ProcessoAvaliacaoConsultaFiller {

        private ProcessoAvaliacaoConsultaForm Form      { get; set; }
        private ProcessoAvaliacaoConsultaVM   ViewModel { get; set; }

        private readonly IProcessoAvaliacaoConsultaBL           ConsultaProcessoAvaliacao;
        private readonly IProcessoAvaliacaoRealizacaoConsultaBL ConsultaProcessoAvaliacaoRealizacao;

        public ProcessoAvaliacaoConsultaFiller(IProcessoAvaliacaoRealizacaoConsultaBL _IProcessoAvaliacaoRealizacaoConsultaBL,
                                               IProcessoAvaliacaoConsultaBL           _IProcessoAvaliacaoConsultaBL) {
            
            ConsultaProcessoAvaliacaoRealizacao = _IProcessoAvaliacaoRealizacaoConsultaBL;
            ConsultaProcessoAvaliacao           = _IProcessoAvaliacaoConsultaBL;
            
            ViewModel = new ProcessoAvaliacaoConsultaVM();
        }

        public ProcessoAvaliacaoConsultaVM carregar(ProcessoAvaliacaoConsultaForm _ProcessoAvaliacaoConsultaForm) {
            Form = _ProcessoAvaliacaoConsultaForm;

            var query = montarQuery();
            
            ViewModel.listaProcessos = carregarProcessos(query);

            return ViewModel;
        }

        internal IQueryable<ProcessoAvaliacao> montarQuery() { // Realiza os filtros de busca e restrição corretamente
            var query =  this.ConsultaProcessoAvaliacao
                             .query(Form.idOrganizacao)
                             .Where(x => x.dtCancelamento == null);
            
            if (Form.ids.Any()) {
                query = query.Where(proc => Form.ids.Contains(proc.id));
            }
            
            if (!Form.valorBusca.isEmpty()) {
                query = query.buscar(Form.valorBusca);
            }
            
            if (Form.idTipoProcesso > 0) {
                query = query.Where(proc => proc.idTipoProcesso == Form.idTipoProcesso.Value);
            }

            return query;
        }
        
        internal IPagedList<ProcessoAvaliacaoDTO> carregarProcessos(IQueryable<ProcessoAvaliacao> query) {
            
            var listaProcessos = query.Select(
                                           x => new {
                                               id             = x.id,
                                               idTipoProcesso = x.idTipoProcesso,
                                               idEtapaFinal   = x.id,
                                               titulo         = x.titulo,
                                               flagOnline     = x.flagOnline,
                                               dtCadastro     = x.dtCadastro,
                                           }
                                      ).OrderBy(x => x.dtCadastro)
                                      .ToPagedListJsonObject<ProcessoAvaliacaoDTO>(Form.nroPagina, Form.nroRegistros);
            
            var idsProcessos = listaProcessos.Select(x => x.id).ToList();

            var listaRealizacoes = this.ConsultaProcessoAvaliacaoRealizacao
                                       .query(Form.idOrganizacao)
                                       .Where(rea => idsProcessos.Contains(rea.idProcessoAvaliacao))
                                       .Select(x => new {
                                           id                       = x.id,
                                           idUnidade                = x.idUnidade,
                                           idProcessoAvaliacao      = x.idProcessoAvaliacao,
                                           flagEtapaPresencial      = x.flagEtapaPresencial,
                                           titulo                   = x.titulo,
                                           qtdeParticipantes        = x.qtdeParticipantes,
                                           flagSomentePagos         = x.flagSomentePagos,
                                           dtRealizacao             = x.dtRealizacao,
                                           horaInicio               = x.horaInicio,
                                           horaFim                  = x.horaFim,
                                           observacoes              = x.observacoes,
                                           qtdeAvaliadores          = x.qtdeAvaliadores,
                                           pontuacaoMinimaAvaliacao = x.pontuacaoMinimaAvaliacao,
                                           pontuacaoMaximaAvaliacao = x.pontuacaoMaximaAvaliacao,
                                           descricaoAprovados       = x.descricaoAprovados,
                                           flagEtapaFinal           = x.flagEtapaFinal,
                                           ativo                    = x.ativo,
                                           dtCadastro               = x.dtCadastro,
                                       }).ToListJsonObject<ProcessoAvaliacaoRealizacaoDTO>();
            
            var listaProcessosComRealizacoes = new List<ProcessoAvaliacaoDTO>();
            
            foreach (var Processo in listaProcessos) {
                Processo.listaRealizacao = listaRealizacoes // esse trecho realiza a classificação das datas de realização dos processos selecionados 
                                                .Where(x => x.idProcessoAvaliacao == Processo.id)
                                                .ToListJsonObject<ProcessoAvaliacaoRealizacaoDTO>() 
                                            ?? new List<ProcessoAvaliacaoRealizacaoDTO>();
                
                Processo.listaRealizacao = Processo.listaRealizacao.OrderBy(x => x.dtCadastro).ToList();
                
                listaProcessosComRealizacoes.Add(Processo);
            }

            return listaProcessosComRealizacoes.ToPagedList(Form.nroPagina, Form.nroRegistros);
        }
    }

}