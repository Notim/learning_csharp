using System;

namespace DAL.Entities {

    public class StatusSolicitacaoMatricula : DefaultEntity {
        public int    id        { get; set; }
        public string descricao { get; set; }
    }

}

