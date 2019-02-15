using System;

namespace DAL.Entities {

    public class SolicitacaoMatricula : DefaultEntity {
        public int      id                   { get; set; }
        public int      idAluno              { get; set; }
        public int      idCoordenador        { get; set; }
        public int      idDisciplinaOfertada { get; set; }
        public DateTime dtSolicitacao        { get; set; }

        public int idStatusSolicitacaoMatricula { get; set; }

        public Aluno                      Aluno                      { get; set; }
        public Coordenador                Coordenador                { get; set; }
        public DisciplinaOfertada         DisciplinaOfertada         { get; set; }
        public StatusSolicitacaoMatricula StatusSolicitacaoMatricula { get; set; }

    }

}