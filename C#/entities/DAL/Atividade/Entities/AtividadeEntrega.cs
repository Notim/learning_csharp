using System;

namespace DAL.Entities {

    public class AtividadeEntrega : DefaultEntity {

        public int      id                       { get; set; }
        public int      idAluno                  { get; set; }
        public int      idProfessor              { get; set; }
        public int      idAtividadeVinculada     { get; set; }
        public int      idStatusEntregaAtividade { get; set; }
        public string   titulo                   { get; set; }
        public string   resposta                 { get; set; }
        public DateTime dtEntrega                { get; set; }
        public float    nota                     { get; set; }
        public DateTime dtAvaliacao              { get; set; }
        public string   obs                      { get; set; }

        public StatusAtividadeEntrega StatusEntregaAtividade { get; set; }
        public AtividadeVinculada     AtividadeVinculada     { get; set; }
        public Aluno                  Aluno                  { get; set; }
        public Professor              Professor              { get; set; }

    }

}