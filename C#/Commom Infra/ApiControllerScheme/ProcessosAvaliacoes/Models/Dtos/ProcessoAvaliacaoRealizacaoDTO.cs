using System;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos {

    public class ProcessoAvaliacaoRealizacaoDTO {

        public int id { get; set; }

        public int? idUnidade { get; set; }

        public int idProcessoAvaliacao { get; set; }

        public bool? flagEtapaPresencial { get; set; }

        public string titulo { get; set; }

        public int? qtdeParticipantes { get; set; }

        public bool? flagSomentePagos { get; set; }

        public DateTime? dtRealizacao { get; set; }

        public string horaInicio { get; set; }

        public string horaFim { get; set; }

        public string observacoes { get; set; }

        public byte? qtdeAvaliadores { get; set; }

        public decimal? pontuacaoMinimaAvaliacao { get; set; }

        public decimal? pontuacaoMaximaAvaliacao { get; set; }

        public string descricaoAprovados { get; set; }

        public bool? flagEtapaFinal { get; set; }

        public bool? ativo { get; set; }

        public DateTime? dtCadastro { get; set; }

    }

}