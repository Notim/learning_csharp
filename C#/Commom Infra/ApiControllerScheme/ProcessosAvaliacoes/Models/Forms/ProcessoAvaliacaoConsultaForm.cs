using System.Collections.Generic;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Forms {
    
    public class ProcessoAvaliacaoConsultaForm {

        public IList<int> ids { get; set; }

        public int? idOrganizacao { get; set; }

        public string valorBusca { get; set; }

        public int? idTipoProcesso { get; set; }

        public int nroPagina { get; set; }

        public int nroRegistros { get; set; }

        public ProcessoAvaliacaoConsultaForm() {
            ids = new List<int>();
        }
    }

}