using System.Collections.Generic;
using System.Linq;

using DAL.Entities;

namespace BLL.Users {

    public interface IUserServices {
        IQueryable<User> Query();
        IList<User>      List();
        User             Charge(int  id);
        bool             Save(User   newUser);
        bool             Exclude(int id);
        bool             Update(User NewUser);
    }

}