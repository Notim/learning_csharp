using DAL.Entities;
using DAL.ModelBuilders;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        public virtual DbSet<User> User { get; set; }

        private void MapperModuleUser(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new UserModelBuilder());

        }
    }

}