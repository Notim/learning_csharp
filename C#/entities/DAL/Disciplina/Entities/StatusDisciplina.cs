using DAL.Entities;

namespace DAL.Entities {

    public class StatusDisciplina : DefaultEntity {
        public int    id        { get; set; }
        public string descricao { get; set; }
    }

}