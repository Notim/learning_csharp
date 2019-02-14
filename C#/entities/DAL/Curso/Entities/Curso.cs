using DAL.Entities;

namespace DAL.Entities {

    public class Curso : DefaultEntity {
        public int    id        { get; set; }
        public string nome      { get; set; }
    }

}