using AguaDeMaria.Model;
using System.Collections.Generic;
using System.Linq;

namespace AguaDeMaria.Common.Data
{
    public class LookupDataManager
    {
        private IRepository<CustomerType> _customerTypeRepository;
        private IRepository<ProductType> _productTypeRepository;
        private IRepository<OrderStatus> _orderSatusRepository;
        private readonly IRepository<ExpenseCategory> expenseCategoryRepository;
        private readonly IRepository<ExpenseType> expenseTypeRepository;

        public LookupDataManager(IRepository<CustomerType> custTypeRepository,
                                 IRepository<ProductType> prodTypeRepository,
                                 IRepository<OrderStatus> orderStatusRepository,
                                 IRepository<ExpenseCategory> expenseCategoryRepository,
                                 IRepository<ExpenseType> expenseTypeRepository)
        {
            _customerTypeRepository = custTypeRepository;
            _productTypeRepository = prodTypeRepository;
            _orderSatusRepository = orderStatusRepository;
            this.expenseCategoryRepository = expenseCategoryRepository;
            this.expenseTypeRepository = expenseTypeRepository;
        }

        public IEnumerable<CustomerType> CustomerTypes
        {
            get
            {
                return _customerTypeRepository.Get(custType => custType.CustomerTypeId > 0).ToList();
            }
        }
        
        public IEnumerable<ExpenseType> ExpenseTypes
        {
            get
            {
                return expenseTypeRepository.Get(e => e.ExpenseTypeId > 0).ToList();
            }
        }

        public IEnumerable<ExpenseCategory> ExpenseCategories
        {
            get
            {
                return expenseCategoryRepository.Get(e => e.ExpenseCategoryId > 0).ToList();
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
