
using System;

namespace DAL.Entities {

    public class DefaultEntity {

        public DateTime dtCadastro { get; set; }

        public DateTime? dtAlteracao { get; set; }

        public DateTime? dtExclusao { get; set; }

        public int idUsuarioCadastro { get; set; }

        public int? idUsuarioAlteracao { get; set; }

        public int? idUsuarioExclusao { get; set; }

        public string ativo { get; set; }

    }

}