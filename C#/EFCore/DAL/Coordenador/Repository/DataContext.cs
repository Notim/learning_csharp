using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        public virtual DbSet<Coordenador> Coordenador { get; set; }

        private void MapperModuloCoordenador(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new CoordenadorModelBuilder());

        }
    }

}