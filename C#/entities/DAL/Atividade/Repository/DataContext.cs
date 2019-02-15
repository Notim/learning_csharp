using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        public virtual DbSet<Atividade>              Atividade              { get; set; }
        public virtual DbSet<AtividadeVinculada>     AtividadeVinculada     { get; set; }
        public virtual DbSet<AtividadeEntrega>       AtividadeEntrega       { get; set; }
        public virtual DbSet<TipoAtividade>          TipoAtividade          { get; set; }
        public virtual DbSet<StatusAtividade>        StatusAtividade        { get; set; }
        public virtual DbSet<StatusAtividadeEntrega> StatusAtividadeEntrega { get; set; }

        private void MapperModuloAtividade(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AtividadeModelBuilder());
            modelBuilder.ApplyConfiguration(new AtividadeVinculadaModelBuilder());
            modelBuilder.ApplyConfiguration(new TipoAtividadeModelBuilder());
            modelBuilder.ApplyConfiguration(new StatusAtividadeModelBuilder());
            modelBuilder.ApplyConfiguration(new AtividadeEntregaModelBuilder());
            modelBuilder.ApplyConfiguration(new StatusAtividadeEntregaModelBuilder());
        }
    }

}