using PagedList;

using System.Collections.Generic;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels {

    public class AreaConhecimentoConsultaVM {

        public IPagedList<AreaConhecimentoDTO> listaAreasConhecimento { get; set; }

        public AreaConhecimentoConsultaVM() {
            listaAreasConhecimento = new List<AreaConhecimentoDTO>().ToPagedList(1, 20);
        }
    }

}