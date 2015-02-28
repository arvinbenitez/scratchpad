using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AguaDeMaria.Common.Data
{
    public interface IRepository<T>
     where T : class
    {
        void Delete(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includedProperties = "");
        void Insert(T entity);
        void Update(T entity);
        void Commit();
    }
}
