using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class ToMigrate : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server = 127.0.0.1,8051; Database = telephonesDB;User Id =SA;Password = TopZera123456");
            }
        }

    }
}

