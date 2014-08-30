using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Common.Data
{
    public interface IUnitOfWork
    {
        void Commit();
        void Delete<T>(T entity) where T : class;
        IRepository<T> GetRepository<T>() where T : class;
        void Insert<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
    }
}
