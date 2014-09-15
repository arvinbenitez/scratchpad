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
        private IRepository<OrderStatus> _orderSatusRepository;
        public LookupDataManager(IRepository<CustomerType> custTypeRepository,
                                 IRepository<ProductType> prodTypeRepository,
                                 IRepository<OrderStatus> orderStatusRepository)
        {
            _customerTypeRepository = custTypeRepository;
            _productTypeRepository = prodTypeRepository;
            _orderSatusRepository = orderStatusRepository;
        }

        public IEnumerable<CustomerType> CustomerTypes
        {
            get
            {
                return _customerTypeRepository.Get(custType => custType.CustomerTypeId > 0).ToList();
            }
        }

        public IEnumerable<ProductType> ProductTypes
        {
            get { return _productTypeRepository.Get(prodType => prodType.ProductTypeId > 0).ToList(); }
        }

        public IEnumerable<OrderStatus> OrderStatuses
        {
            get
            {
                return _orderSatusRepository.Get(s => s.OrderStatusId > 0, s => s.OrderBy(t => t.StatusName)).ToList();
            }
        }

    }
}
