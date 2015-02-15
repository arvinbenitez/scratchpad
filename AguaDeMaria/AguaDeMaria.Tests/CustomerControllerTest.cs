#region

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Controllers;
using AguaDeMaria.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

#endregion

namespace AguaDeMaria.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController customerController;
        private Mock<IRepository<Customer>> customerRepositoryMock;
        private Mock<IRepository<CustomerType>> customerTypeRepositoryMock;
        private Mock<LookupDataManager> lookupManagerMock;
        private Mock<IRepository<OrderStatus>> orderSatusRepositoryMock;
        private Mock<IRepository<ProductType>> productTypeRepositoryMock;

        [TestInitialize]
        public void OnInit()
        {
            //TODO: Refactor
            customerRepositoryMock = new Mock<IRepository<Customer>>();
            customerTypeRepositoryMock = new Mock<IRepository<CustomerType>>();
            productTypeRepositoryMock = new Mock<IRepository<ProductType>>();
            orderSatusRepositoryMock = new Mock<IRepository<OrderStatus>>();
            lookupManagerMock =
                new Mock<LookupDataManager>(new object[]
                {
                    customerTypeRepositoryMock.Object, productTypeRepositoryMock.Object,
                    orderSatusRepositoryMock.Object
                });
            customerController = new CustomerController(customerRepositoryMock.Object, lookupManagerMock.Object);
        }

        [TestMethod]
        public void WhenIndexIsCalledShouldReturnList()
        {
            customerRepositoryMock.Setup(x => x.Get(y => y.CustomerId > 0, null, string.Empty))
                .Returns(new List<Customer> {new Customer {CustomerId = 100}});
            var result = customerController.Index();
            Assert.IsNotNull(result);
        }
    }
}