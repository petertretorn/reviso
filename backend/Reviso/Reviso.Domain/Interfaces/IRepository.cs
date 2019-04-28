using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reviso.Domain.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);

        void Update(T t);

        void Delete(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes);

        T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includes);

        int Commit();

    }
}
