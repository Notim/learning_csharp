using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class EntitiesCore : DbContext {

        public virtual DbSet<Usuario>     Usuario     { get; set; }
        public virtual DbSet<Coordenador> Coordenador { get; set; }
        public virtual DbSet<Aluno>       Aluno       { get; set; }
        public virtual DbSet<Professor>   Professor   { get; set; }

    }

}