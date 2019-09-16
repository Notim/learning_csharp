using PagedList;

using System.Collections.Generic;

using WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.ViewModels {

    public class InscricaoEtapaConsultaVM {

        public IPagedList<InscricaoEtapaDTO> listaAprovados { get; set; }

        public InscricaoEtapaConsultaVM() {
            listaAprovados = new List<InscricaoEtapaDTO>().ToPagedList(1, 20);
        }
    }

}