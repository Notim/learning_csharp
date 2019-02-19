using DAL.Configuration;

using Microsoft.EntityFrameworkCore;

namespace DAL.Entities {

    public class DefaultContext {
        protected readonly DataContext db = new DataContext("StdSqlServer");
    }

}