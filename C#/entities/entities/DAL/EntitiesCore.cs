using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class EntitiesCore : DbContext {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseSqlite("Data Source =DATABASE.db");
            }
        }

    }

}