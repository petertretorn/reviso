using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reviso.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T GetById(int id);

        T Add(T t);

        void Update(T t);

        void Delete(int id);

        void Delete(T t);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes);

        T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includes);

        int Commit();

    }
}
