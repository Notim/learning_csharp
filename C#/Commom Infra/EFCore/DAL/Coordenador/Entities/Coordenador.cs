using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Coordenador : DefaultEntity {
        public int    id        { get; set; }
        public int    idUsuario { get; set; }
        public string nome      { get; set; }
        public string email     { get; set; }
        public string celular   { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

}