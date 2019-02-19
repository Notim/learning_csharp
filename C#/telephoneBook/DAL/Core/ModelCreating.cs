using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            this.MapperModuleUser(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }

}