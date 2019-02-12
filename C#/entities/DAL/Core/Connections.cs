using System.Collections.Generic;

namespace DAL.Configuration {

    public class Connections {
        
        public static Connections GetInstance => new Connections(); 
        
        public IList<Connection> listConnections { get; set; }
        
        public Connections() {
            listConnections = new List<Connection>();
            
            listConnections.Add(
                new Connection {
                    name   = "StdSqlite",
                    strConnection = @"Data Source =DATABASE.db",
                    provider = "SqlLite"
                }
            );
            listConnections.Add(
                new Connection {
                   name   = "StdSqlServer",
                   strConnection = @"Server=127.0.0.1,8051; Database=Master;User Id=SA;Password=TopZera123456",
                   provider = "SqlServer"
                }
            );
        }
    }
    
    public class Connection {
        public string name          { get; set; }
        public string strConnection { get; set; }
        public string provider      { get; set; }
    }

}