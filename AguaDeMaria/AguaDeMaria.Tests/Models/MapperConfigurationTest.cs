using AguaDeMaria.Model;
using AguaDeMaria.Models;
using AguaDeMaria.Models.Order;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AguaDeMaria.Tests.Models
{
    [TestClass]
    public class MapperConfigurationTest
    {
        [TestInitialize]
        public void Setup()
        {
            MapperConfiguration.Configure();
        }

        [TestMethod]
        public void WhenOrderIsMappedToDto_Should_GetTotalDeliries()
        {
            var order = new Order
            {
                OrderDetails = new[]
                {
                    new OrderDetail()
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail()
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Round,
                        Qty = 5
                    }
                },
                DeliveryReceipts = new[]
                {
                    new DeliveryReceipt
                    {
                        DeliveryReceiptDetails = new[]
                        {
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 1,
                                ProductTypeId = DataConstants.ProductTypes.Slim,
                                Quantity = 2
                            },
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 1,
                                ProductTypeId = DataConstants.ProductTypes.Round,
                                Quantity = 2
                            }
                        }
                    },
                    new DeliveryReceipt
                    {
                        DeliveryReceiptDetails = new[]
                        {
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 3,
                                ProductTypeId = DataConstants.ProductTypes.Slim,
                                Quantity = 3
                            },
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 4,
                                ProductTypeId = DataConstants.ProductTypes.Round,
                                Quantity = 2
                            }
                        }
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);

            Assert.AreEqual(5, orderDto.SlimQtyBalance);
            Assert.AreEqual(1, orderDto.RoundQtyBalance);

        }

        [TestMethod]
        public void WhenOrderIsMappedToDto_And_ThereAreNoDeliveries_Should_ReturnZero()
        {
            var order = new Order
            {
                OrderDetails = new[]
                {
                    new OrderDetail()
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail()
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Round,
                        Qty = 5
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);

            Assert.AreEqual(0, orderDto.SlimQtyDelivered);
            Assert.AreEqual(0, orderDto.RoundQtyDelivered);

        }

    }
}