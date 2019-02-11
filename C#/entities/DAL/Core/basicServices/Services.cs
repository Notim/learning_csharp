using System.Collections.Generic;

using DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration.basicServices {

    public interface IServices {
        void     Add<T>(T Entity);
        IList<T> List<T>();
        // T        Charge<T>(int id);
    }

    public class Services : DefaultContext, IServices {
        public void Add<T>(T Entity) {
            var type = typeof (T); 
            
        }

        public IList<T> List<T>() {
            return new List<T>();
        }

        /*
        public T Charge<T>(int id) {
            
            return null;
        }*/
    }

}