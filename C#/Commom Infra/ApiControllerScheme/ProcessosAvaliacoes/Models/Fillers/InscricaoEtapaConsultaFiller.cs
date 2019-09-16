using System.Collections.Generic;
using System.Linq;
using BLL.ProcessosAvaliacoes;
using BLL.Services;
using DAL.ProcessosAvaliacoes;
using PagedList;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers {

    public class InscricaoEtapaConsultaFiller {

        private InscricaoEtapaConsultaForm Form      { get; set; }
        private InscricaoEtapaConsultaVM   ViewModel { get; set; }
        
        private readonly IProcessoAvaliacaoInscricaoEtapaConsultaBL ConsultaInscricaoEtapa;

        public InscricaoEtapaConsultaFiller(IProcessoAvaliacaoInscricaoEtapaConsultaBL _IProcessoAvaliacaoInscricaoEtapaConsultaBL) {
            
            ConsultaInscricaoEtapa = _IProcessoAvaliacaoInscricaoEtapaConsultaBL;
            
            ViewModel = new InscricaoEtapaConsultaVM();
        }

        public InscricaoEtapaConsultaVM carregar(InscricaoEtapaConsultaForm _AreaConhecimentoConsultaForm) {
            Form = _AreaConhecimentoConsultaForm;

            var query = montarQuery();
            
            ViewModel.listaAprovados = carregarAprovados(query);

            return ViewModel;
        }

        internal IQueryable<ProcessoAvaliacaoInscricaoEtapa> montarQuery() { // Realiza os filtros de busca e restrição corretamente
            var query = this.ConsultaInscricaoEtapa
                            .query(Form.idOrganizacao)
                            .Where(x => x.idProcessoAvaliacao == Form.idProcessoAvaliacao && 
                                        x.idEtapa == Form.idEtapa && 
                                        x.ProcessoAvaliacaoInscricao.dtCancelamento == null
                            );

            if (Form.flagAprovados.HasValue) {
                query = query.Where(x => (x.dtAprovacao.HasValue == Form.flagAprovados.Value) || (x.flagAprovado.Value == Form.flagAprovados.Value));
            }
            
            if (Form.idsAreasConhecimento.Any()) {
                query = query.Where(x => Form.idsAreasConhecimento.Contains((int) x.ProcessoAvaliacaoInscricao.idAreaConhecimento));
            }
            
            return query;
        }
        
        internal IPagedList<InscricaoEtapaDTO> carregarAprovados(IQueryable<ProcessoAvaliacaoInscricaoEtapa> query) {
            
            var listaAprovados = query.Select(
                                           x => new InscricaoEtapaDTO {
                                              id           = x.id,
                                              idIncricao   = x.idInscrito,
                                              nomeInscrito = x.ProcessoAvaliacaoInscricao.nomeInscrito,
                                              titulo       = x.ProcessoAvaliacaoInscricao.titulo,
                                           }
                                       ).ToListJsonObject<InscricaoEtapaDTO>() ?? new List<InscricaoEtapaDTO>();
            
            listaAprovados.ForEach(c => { c.nomeCientifico = c.nomeInscrito.exibirNomeCientifico(); });
                
            return listaAprovados.ToPagedList(Form.nroPagina, Form.nroRegistros);
        }
    }

}