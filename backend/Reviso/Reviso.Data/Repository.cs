using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Reviso.Domain.Interfaces;

namespace Reviso.Data
{
    public class Repository<T> : IRepository<T> where T : IEntity, new()
    {
        private DbContext _context;
        private bool _disposed;
        protected DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public T GetById(int id) => _dbSet.FirstOrDefault(t => t.Id == id);


        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _dbSet.AsNoTracking();

            return AttachIncludes(includes, queryable).ToList();
        }

        public T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _dbSet.Where(t => t.Id == id);

            return AttachIncludes(includes, queryable).First();
        }

        private static IQueryable<T> AttachIncludes(Expression<Func<T, object>>[] includes, IQueryable<T> queryable)
        {
            return includes
                .Aggregate(queryable,
                    (current, includeProperty) => current.Include(includeProperty));
        }

        public T Add(T t)
        {
            _dbSet.Add(t);
            _context.SaveChanges();

            return t;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbSet.Remove(new T { Id = id });
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
            _context.SaveChanges();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
