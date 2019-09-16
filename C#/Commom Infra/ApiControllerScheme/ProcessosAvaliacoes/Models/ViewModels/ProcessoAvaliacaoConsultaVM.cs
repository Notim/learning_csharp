using PagedList;

using System.Collections.Generic;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels {

    public class ProcessoAvaliacaoConsultaVM {

        public IPagedList<ProcessoAvaliacaoDTO> listaProcessos { get; set; }

        public ProcessoAvaliacaoConsultaVM() {
            listaProcessos = new List<ProcessoAvaliacaoDTO>().ToPagedList(1, 20);
        }
    }

}