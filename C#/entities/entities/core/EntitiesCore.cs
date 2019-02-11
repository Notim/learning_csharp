using entities.core.Entities;

using Microsoft.EntityFrameworkCore;

namespace entities.core {

    public partial class EntitiesCore : DbContext {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseSqlite("Data Source =DATABASE.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new ClassroomModelBuilder());
            modelBuilder.ApplyConfiguration(new PersonModelBuilder());

        }

    }

}