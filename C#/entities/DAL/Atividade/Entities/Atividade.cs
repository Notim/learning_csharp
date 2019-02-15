using DAL.Entities;

namespace DAL.Entities {

    public class Atividade : DefaultEntity {
        public int id              { get; set; }
        public int idProfessor     { get; set; }
        public int idTipoAtividade { get; set; }

        public string titulo    { get; set; }
        public string descricao { get; set; }
        public string extras    { get; set; }

        public Professor     Professor     { get; set; }
        public TipoAtividade TipoAtividade { get; set; }
    }

}