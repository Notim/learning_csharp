using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        public virtual DbSet<Aluno> Aluno { get; set; }

        private void MapperModuloAluno(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AlunoModelBuilder());
        }
    }

}