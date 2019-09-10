using System;

namespace DAL.Entities {

    public class DisciplinaOfertada : DefaultEntity {
        public int      id                { get; set; }
        public int      idCoordenador     { get; set; }
        public int      idProfessor       { get; set; }
        public int      idDisciplina      { get; set; }
        public int      idCurso           { get; set; }
        public DateTime dtInicioMatricula { get; set; }
        public DateTime dtFimMatricula    { get; set; }
        public DateTime ano               { get; set; }
        public int      semestre          { get; set; }
        public string   turma             { get; set; }
        public string   metodologia       { get; set; }
        public string   recursos          { get; set; }
        public string   criterioAvaliacao { get; set; }
        public string   planoAulas        { get; set; }

        public Professor  Professor  { get; set; }
        public Disciplina Disciplina { get; set; }
        public Curso      Curso      { get; set; }
    }

}