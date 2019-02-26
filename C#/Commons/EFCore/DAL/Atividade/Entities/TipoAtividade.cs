using DAL.Entities;

namespace DAL.Entities {

    public class TipoAtividade : DefaultEntity {
        public int    id        { get; set; }
        public string descricao { get; set; }
    }

}