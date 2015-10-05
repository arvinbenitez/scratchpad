using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CIT.CAB.Web.Entity
{
    public interface IGenericRepository<TEntity> where TEntity : EntityObject
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

    /// <summary>
    /// Generic class implementing the Repository pattern.
    /// This should be used to query the data store instead of going to the DataContext directly.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityObject
    {
        private MessageBrokerContext _messageBrokerContext;
        private ObjectSet<TEntity> _dbSet;

        public GenericRepository(MessageBrokerContext messageBrokerContext)
        {
            _messageBrokerContext = messageBrokerContext;
            _dbSet = messageBrokerContext.CreateObjectSet<TEntity>();
        }

        public GenericRepository()
            : this(new MessageBrokerContext())
        {
        }

        #region Data Query
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IEnumerable<TEntity> result;

            ObjectQuery<TEntity> query = _dbSet;
            foreach (var includedProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperty);
            }

            IQueryable<TEntity> filteredQuery = query;
            if (filter != null)
            {
                filteredQuery = filteredQuery.Where(filter);
            }

            if (orderBy != null)
            {
                result = orderBy(filteredQuery).ToList();
            }
            else
            {
                result = filteredQuery.ToList();
            }
            return result;
        }
        #endregion

        #region Data Manipulation
        public void Insert(TEntity entity)
        {
            _dbSet.AddObject(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _messageBrokerContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        public void Delete(TEntity entity)
        {
            if (_messageBrokerContext.ObjectStateManager.GetObjectStateEntry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.DeleteObject(entity);
        }
        #endregion Data Manipulation
    }

}
