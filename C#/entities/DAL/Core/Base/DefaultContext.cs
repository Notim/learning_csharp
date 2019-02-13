using DAL.Configuration;

using Microsoft.EntityFrameworkCore;

namespace DAL.Entities {

    public class DefaultContext {
        private DataContext db = new DataContext("StdSqlServer");
    }

}