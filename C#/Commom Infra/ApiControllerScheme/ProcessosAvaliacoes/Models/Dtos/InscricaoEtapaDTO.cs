using System.Collections.Generic;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos {

    public class InscricaoEtapaDTO {

        public int? id { get; set; }

        public int? idIncricao { get; set; }

        public string nomeInscrito { get; set; }

        public string nomeCientifico { get; set; }

        public string titulo { get; set; }

        public IList<InscricaoEtapaDTO> listaInscricoesFilhas { get; set; }

        public InscricaoEtapaDTO() {
            this.listaInscricoesFilhas = new List<InscricaoEtapaDTO>();
        }
    }

}