using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
 
        public virtual DbSet<Usuario> Usuario { get; set; }

        private void MapperModuloUsuario(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new UsuarioModelBuilder());

        }
    }

}