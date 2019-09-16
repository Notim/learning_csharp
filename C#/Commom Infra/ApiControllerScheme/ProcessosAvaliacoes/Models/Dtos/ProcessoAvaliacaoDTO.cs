using System;
using System.Collections.Generic;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos {

    public class ProcessoAvaliacaoDTO {

        public int? id { get; set; }

        public int? idTipoProcesso { get; set; }

        public int? idEtapaFinal { get; set; }

        public string titulo { get; set; }

        public bool flagOnline { get; set; }

        public DateTime? dtCadastro { get; set; }

        public IList<ProcessoAvaliacaoRealizacaoDTO> listaRealizacao { get; set; }

        public ProcessoAvaliacaoDTO() {
            listaRealizacao = new List<ProcessoAvaliacaoRealizacaoDTO>();
        }
    }

}