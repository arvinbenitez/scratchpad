using System;
using System.Collections.Generic;
using AguaDeMaria.Model;

namespace AguaDeMaria.Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AguaDeMariaContext _context;
        private IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(AguaDeMariaContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }

}
