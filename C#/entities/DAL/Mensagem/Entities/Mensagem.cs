using System;

using DAL.Entities;

namespace DAL.Entities {

    public class Mensagem : DefaultEntity {

        public int    id               { get; set; }
        public int    idAluno          { get; set; }
        public int    idProfessor      { get; set; }
        public string idStatusMensagem { get; set; }
        public string assunto          { get; set; }
        public string referencias      { get; set; }
        public string conteudo         { get; set; }

        public DateTime dtEnvio    { get; set; }
        public DateTime dtResposta { get; set; }
        public string   resposta   { get; set; }

        public Aluno     Aluno     { get; set; }
        public Professor Professor { get; set; }

        public StatusAtividade StatusMensagem { get; set; }

    }


}