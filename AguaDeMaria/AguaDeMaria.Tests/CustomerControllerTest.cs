#region

using System.Collections.Generic;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Controllers;
using AguaDeMaria.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

#endregion

namespace AguaDeMaria.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController _customerController;
        private Mock<IRepository<Customer>> _customerRepositoryMock;
        private Mock<IRepository<CustomerType>> _customerTypeRepositoryMock;
        private Mock<LookupDataManager> _lookupManagerMock;
        private Mock<IRepository<OrderStatus>> _orderSatusRepositoryMock;
        private Mock<IRepository<ProductType>> _productTypeRepositoryMock;

        [TestInitialize]
        public void OnInit()
        {
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _customerTypeRepositoryMock = new Mock<IRepository<CustomerType>>();
            _productTypeRepositoryMock = new Mock<IRepository<ProductType>>();
            _orderSatusRepositoryMock = new Mock<IRepository<OrderStatus>>();
            _lookupManagerMock =
                new Mock<LookupDataManager>(new object[]
                {
                    _customerTypeRepositoryMock.Object, _productTypeRepositoryMock.Object,
                    _orderSatusRepositoryMock.Object
                });
            _customerController = new CustomerController(_customerRepositoryMock.Object, _lookupManagerMock.Object);
        }

        [TestMethod]
        public void WhenIndexIsCalledShouldReturnList()
        {
            _customerRepositoryMock.Setup(x => x.Get(y => y.CustomerId > 0, null, string.Empty))
                .Returns(new List<Customer> {new Customer()});
            var result = _customerController.Index();
            Assert.IsNotNull(result);
        }
    }
}