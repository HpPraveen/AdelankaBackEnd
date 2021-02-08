using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Adelanka.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal AdelankaDBContext _context;
        internal DbSet<T> table;

        public GenericRepository(AdelankaDBContext adelankaDBContext)
        {
            _context = adelankaDBContext;
            table = adelankaDBContext.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", Expression<Func<T, bool>>[] filters = null)
        {
            IQueryable<T> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (filters != null)
            {
                foreach (var expression in filters)
                {
                    if (expression != null)
                    {
                        query = query.Where(expression);
                    }
                }
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T objToDelete = table.Find(id);
            table.Remove(objToDelete);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}