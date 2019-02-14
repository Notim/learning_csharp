using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
        public virtual DbSet<Disciplina> Disciplina { get; set; }

        public virtual DbSet<StatusDisciplina> StatusDisciplina { get; set; }

        public virtual DbSet<DisciplinaOfertada> DisciplinaOfertada { get; set; }

        private void MapperModuloDisciplina(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new DisciplinaModelBuilder());

            modelBuilder.ApplyConfiguration(new StatusDisciplinaModelBuilder());

            modelBuilder.ApplyConfiguration(new DisciplinaOfertadaModelBuilder());
        }
    }

}