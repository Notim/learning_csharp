using System.Collections.Generic;
using System.Linq;

using BLL.ProcessosAvaliacoes.Interfaces;
using DAL.ProcessosAvaliacoes.Entities;

using PagedList;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms;
using WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels;
using WEB.Extensions;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Fillers {

    public class AreaConhecimentoConsultaFiller {

        private AreaConhecimentoConsultaForm Form      { get; set; }
        private AreaConhecimentoConsultaVM   ViewModel { get; set; }

        private readonly IProcessoAvaliacaoAreaConhecimentoConsultaBL ConsultaAreaConhecimento;

        public AreaConhecimentoConsultaFiller(IProcessoAvaliacaoAreaConhecimentoConsultaBL _IProcessoAvaliacaoAreaConhecimentoConsultaBL) {
            ConsultaAreaConhecimento = _IProcessoAvaliacaoAreaConhecimentoConsultaBL;
            
            ViewModel = new AreaConhecimentoConsultaVM();
        }

        public AreaConhecimentoConsultaVM carregar(AreaConhecimentoConsultaForm _AreaConhecimentoConsultaForm) {
            Form = _AreaConhecimentoConsultaForm;

            var query = montarQuery();
            
            ViewModel.listaAreasConhecimento = carregarAreasConhecimento(query);

            return ViewModel;
        }

        internal IQueryable<ProcessoAvaliacaoAreaConhecimento> montarQuery() { // Realiza os filtros de busca e restri��o corretamente
            var query =  this.ConsultaAreaConhecimento
                             .query(Form.idOrganizacao)
                             .Where(area => area.idProcessoAvaliacao == Form.idProcessoAvaliacao);
            
            return query;
        }
        
        internal IPagedList<AreaConhecimentoDTO> carregarAreasConhecimento(IQueryable<ProcessoAvaliacaoAreaConhecimento> query) {
            var lista = query.Select(
                                  x => new {
                                      id        = x.idAreaConhecimento,
                                      descricao = x.AreaConhecimento.descricao,
                                      idProcessoAvaliacao       = x.idProcessoAvaliacao
                                  }
                              ).OrderBy(x => x.descricao)
                              .ToPagedListJsonObject<AreaConhecimentoDTO>(Form.nroPagina, Form.nroRegistros);
            
            return lista ?? new List<AreaConhecimentoDTO>().ToPagedList(Form.nroPagina, Form.nroRegistros);
        }
    }

}