using AguaDeMaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Common.Data
{
    public class LookupDataManager
    {
        private IRepository<CustomerType> _customerTypeRepository;
        public LookupDataManager(IRepository<CustomerType> custTypeRepository)
        {
            _customerTypeRepository = custTypeRepository;
        }

        public IEnumerable<CustomerType> CustomerTypes
        {
            get
            {
                return _customerTypeRepository.Get(custType => custType.CustomerTypeId > 0);
            }
        }
    }
}
