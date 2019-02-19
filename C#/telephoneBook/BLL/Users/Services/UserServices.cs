using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using DAL.Entities;

namespace BLL.Users.Services {

    public class UserServices : DefaultContext, IUserServices {
        public IQueryable<User> Query() {
            var query = db.User;
            
            return query;
        }

        public IList<User> List() {
            
            return this.Query().ToList();
        }

        public User Charge(int id) {
            var TempUser = this.Query().FirstOrDefault(x => x.id == id);

            return TempUser ?? new User();
        }

        public bool Save(User NewUser) {
            db.User.Add(NewUser);
            
            db.SaveChanges();
            
            return true;
        }
        
        public bool Update(User NewUser) {
            var Old = db.User.FirstOrDefault(x => x.id == NewUser.id);

            if (Old == null) {
                return false;
            }
            
            Old = NewUser;
            
            db.User.Update(Old);

            db.SaveChanges();
            
            return true;
        }

        public bool Exclude(int id) {
            var ToExclude = db.User.Find(id);
            db.User.Remove(ToExclude);
            
            db.SaveChanges();
            
            return true;
        }
    }

}