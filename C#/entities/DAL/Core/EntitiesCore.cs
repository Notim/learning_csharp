using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            
            if (!optionsBuilder.IsConfigured) 
                optionsBuilder.UseSqlite("Data Source =DATABASE.db");
            
        }

    }

}