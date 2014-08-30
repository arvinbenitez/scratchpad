using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AguaDeMaria.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AguaDeMaria.Common.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private AquaKeshContext _context;

        private DbSet<T> _dbSet;

        public GenericRepository(AquaKeshContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  string includedProperties = "")
        {
            IEnumerable<T> result;
            DbQuery<T> query = _dbSet;

            //Tell the query to include a property
            foreach (var includedProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperty);
            }

            //Add the Filter
            IQueryable<T> filteredQuery = query;
            if (filter != null)
            {
                filteredQuery = filteredQuery.Where(filter);
            }

            if (orderBy != null)
            {
                //Order the results
                result = orderBy(filteredQuery).ToList();
            }
            else
            {
                //No order, just return the results
                result = filteredQuery.ToList();
            }
            return result;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (!ExistsInRepository(entity))
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            if (!ExistsInRepository(entity))
            {
                _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }

        private bool ExistsInRepository(T entity)
        {
            return _dbSet.Local.Any(x => x == entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public AquaKeshContext Context
        {
            get { return _context; }
        }

    }
}
