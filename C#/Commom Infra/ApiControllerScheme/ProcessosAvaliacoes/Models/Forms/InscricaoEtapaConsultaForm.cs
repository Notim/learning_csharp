using System.Collections.Generic;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms {

    public class InscricaoEtapaConsultaForm {

        public int idOrganizacao { get; set; }

        public int idProcessoAvaliacao { get; set; }

        public IList<int> idsAreasConhecimento { get; set; }

        public bool? flagAprovados { get; set; }

        public int idEtapa { get; set; }

        public int nroPagina { get; set; }

        public int nroRegistros { get; set; }
    }

}