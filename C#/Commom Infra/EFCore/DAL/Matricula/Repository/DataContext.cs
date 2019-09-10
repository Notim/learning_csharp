using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
        public virtual DbSet<SolicitacaoMatricula> SolicitacaoMatricula { get; set; }

        public virtual DbSet<StatusSolicitacaoMatricula> StatusSolicitacaoMatricula { get; set; }

        private void MapperModuloMatricula(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new SolicitacaoMatriculaModelBuilder());

            modelBuilder.ApplyConfiguration(new StatusSolicitacaoMatriculaModelBuilder());

        }
    }

}