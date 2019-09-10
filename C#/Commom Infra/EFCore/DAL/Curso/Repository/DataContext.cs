using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
        public virtual DbSet<Curso> Curso { get; set; }

        private void MapperModuloCurso(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new CursoModelBuilder());
        }
    }

}