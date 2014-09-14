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
        private IRepository<ProductType> _productTypeRepository;
        public LookupDataManager(IRepository<CustomerType> custTypeRepository,IRepository<ProductType> prodTypeRepository)
        {
            _customerTypeRepository = custTypeRepository;
            _productTypeRepository = prodTypeRepository;
        }

        public IEnumerable<CustomerType> CustomerTypes
        {
            get
            {
                return _customerTypeRepository.Get(custType => custType.CustomerTypeId > 0) .ToList();
            }
        }

        public IEnumerable<ProductType> ProductTypes
        {
            get { return _productTypeRepository.Get(prodType => prodType.ProductTypeId > 0).ToList(); }
        }
    }
}
