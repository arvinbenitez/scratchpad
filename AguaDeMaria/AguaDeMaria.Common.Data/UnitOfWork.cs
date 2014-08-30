using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AguaDeMaria.Database;

namespace AguaDeMaria.Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AquaKeshContext _context;
        private IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(AquaKeshContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            GenericRepository<T> repository;
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories.Add(typeof(T), new GenericRepository<T>(_context));
            }

            repository = (GenericRepository<T>)_repositories[typeof(T)];
            return repository;
        }

        public void Insert<T>(T entity) where T : class
        {
            GenericRepository<T> repository = (GenericRepository<T>)_repositories[typeof(T)];
            repository.Insert(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            GenericRepository<T> repository = (GenericRepository<T>)_repositories[typeof(T)];
            repository.Delete(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            GenericRepository<T> repository = (GenericRepository<T>)_repositories[typeof(T)];
            repository.Update(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }

}
