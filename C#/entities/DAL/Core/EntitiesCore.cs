using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        private IList<Connection> listConnections { get; set; }
        private Connection        Connection      { get; set; }

        public DataContext(string connectionName) {
            var ConnectioBuilder = new Connections();

            listConnections = ConnectioBuilder.listConnections;

            Connection = listConnections.FirstOrDefault(x => x.name == connectionName) ?? new Connection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {

                switch (Connection.provider) {
                    case "SqlLite": {
                        optionsBuilder.UseSqlite(Connection.strConnection);

                        break;
                    }

                    case "SqlServer": {
                        optionsBuilder.UseSqlServer(Connection.strConnection);

                        break;
                    }

                    case "MySql": {
                        optionsBuilder.UseMySql(Connection.strConnection);

                        break;
                    }
                }

            }
        }
    }

}