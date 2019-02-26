using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
        public virtual DbSet<Professor> Professor { get; set; }

        private void MapperModuloProfessor(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new ProfessorModelBuilder());

        }
    }

}