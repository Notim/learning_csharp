using DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class EntitiesCore : DbContext {

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new UsuarioModelBuilder());
            modelBuilder.ApplyConfiguration(new CoordenadorModelBuilder());

        }
    }

}