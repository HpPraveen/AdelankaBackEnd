using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Adelanka.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", Expression<Func<T, bool>>[] filters = null);

        T GetById(object id);

        void Insert(T entity);

        void Update(T entityToUpdate);

        void Delete(object id);

        void Save();
    }
}