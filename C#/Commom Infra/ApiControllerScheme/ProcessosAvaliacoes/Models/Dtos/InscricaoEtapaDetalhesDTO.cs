using System;

namespace WEB.Areas.Api.ProcessosAvaliacoes.Models.Dtos {

    public class InscricaoEtapaDetalhesDTO {
        public int id { get; set; }

        public int? idIncricao { get; set; }

        public string nomeInscrito { get; set; }

        public DateTime? dtAprovacao { get; set; }

        public bool? flagAprovado { get; set; }

        public DateTime? dtReprovacao { get; set; }

        public DateTime? dtFinalizacao { get; set; }

        public DateTime? dtFinalizacaoAvaliacao { get; set; }

        public DateTime?  dtFinalizacaoPreAnalise { get; set; }

        public string tituloTrabalho { get; set; }

        public string areaConhecimento { get; set; }

        public string linkArquivoTrabalho { get; set; }

        public string nomeInstituicao { get; set; }

        public string objetivo { get; set; }

        public string metodo { get; set; }

        public string conclusao { get; set; }

        public string referencia { get; set; }
    }

}