using System.Collections.Generic;

using Core.SharedKernel;

namespace Core.Interface {

    public interface IRepository<T> where T : BaseEntity {

        IUnitOfWork UnitOfWork { get; }

        T GetById(int id);

        List<T> List();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }

}