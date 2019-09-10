using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {
        public virtual DbSet<Mensagem> Mensagem { get; set; }

        public virtual DbSet<StatusMensagem> StatusMensagem { get; set; }

        private void MapperModuloMensagem(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new MensagemModelBuilder());

            modelBuilder.ApplyConfiguration(new StatusMensagemModelBuilder());

        }
    }

}