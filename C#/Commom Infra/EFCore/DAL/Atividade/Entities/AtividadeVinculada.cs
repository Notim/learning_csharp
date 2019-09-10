using System;

namespace DAL.Entities {

    public class AtividadeVinculada : DefaultEntity {
        public int id                   { get; set; }
        public int idProfessor          { get; set; }
        public int idAtividade          { get; set; }
        public int idDisciplinaOfertada { get; set; }
        public int idStatusAtividade    { get; set; }

        public string   rotulo            { get; set; }
        public DateTime dtinicioResposta  { get; set; }
        public DateTime dtPrimeiraReposta { get; set; }

        public Professor          Professor          { get; set; }
        public DisciplinaOfertada DisciplinaOfertada { get; set; }
        public StatusAtividade    StatusAtividade    { get; set; }
        public Atividade          Atividade          { get; set; }

    }

}