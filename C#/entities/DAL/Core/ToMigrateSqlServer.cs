using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext2 : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server = 127.0.0.1,8051; Database = Lms2019;User Id =SA;Password = TopZera123456");
            }
        }

    }

}