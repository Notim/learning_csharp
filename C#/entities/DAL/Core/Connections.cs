using System.Collections.Generic;

namespace DAL.Configuration {

    public class Connections {
        
        public IList<Connection> listConnections { get; set; }
        
        public Connections() {
            listConnections = new List<Connection>();
            
            listConnections.Add(
                new Connection {
                    name          = "StdSqlite",
                    strConnection = @"Data Source =DATABASE.db",
                    provider      = "SqlLite"
                }
            );
            listConnections.Add(
                new Connection {
                   name          = "StdSqlServer",
                   strConnection = @"Server=127.0.0.1,8051; Database=Lms2019;User Id=SA;Password=TopZera123456",
                   provider      = "SqlServer"
                }
            );
            listConnections.Add(
                new Connection {
                    name          =  "StdMysql",
                    strConnection =  @"Server=127.0.0.1;Port=8050;Database=Lms2019;Uid=root;Pwd=TopZera12356;",
                    provider      =  "MySql"
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