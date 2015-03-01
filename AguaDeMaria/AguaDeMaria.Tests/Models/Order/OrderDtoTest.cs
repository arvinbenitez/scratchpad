using AguaDeMaria.Configuration.Mappers;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Order;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AguaDeMaria.Tests.Models.Order
{
    [TestClass]
    public class OrderDtoTest
    {
        [TestInitialize]
        public void Setup()
        {
            MapperConfiguration.Configure();
        }

        [TestMethod]
        public void When_Order_Is_Delivered_The_OrderStatus_Should_Be_Delivered()
        {
            var order = new Model.Order
            {
                OrderDetails = new[]
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail
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
                                Quantity = 8
                            },
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 4,
                                ProductTypeId = DataConstants.ProductTypes.Round,
                                Quantity = 3
                            }
                        }
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);
            Assert.AreEqual(DataConstants.OrderStatus.Delivered, orderDto.CalculatedStatusId);
        }

        [TestMethod]
        public void When_Order_Is_Partially_Delivered_The_OrderStatus_Should_Be_Correct()
        {
            var order = new Model.Order
            {
                OrderDetails = new[]
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail
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
                                Quantity = 0
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
                                Quantity = 8
                            },
                            new DeliveryReceiptDetail
                            {
                                DeliveryReceiptDetailId = 4,
                                ProductTypeId = DataConstants.ProductTypes.Round,
                                Quantity = 3
                            }
                        }
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);
            Assert.AreEqual(DataConstants.OrderStatus.PartiallyDelivered, orderDto.CalculatedStatusId);
        }

        [TestMethod]
        public void When_Order_Has_No_Deliveries_The_OrderStatus_Should_Be_Correct()
        {
            var order = new Model.Order
            {
                OrderDetails = new[]
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Round,
                        Qty = 5
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);
            Assert.AreEqual(DataConstants.OrderStatus.Pending, orderDto.CalculatedStatusId);
        }

        [TestMethod]
        public void When_Order_Has_Been_Cancelled_The_OrderStatus_Should_Be_Correct()
        {
            var order = new Model.Order
            {
                OrderStatusId = DataConstants.OrderStatus.Cancelled,
                OrderDetails = new[]
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Slim,
                        Qty = 10
                    },
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        ProductTypeId = DataConstants.ProductTypes.Round,
                        Qty = 5
                    }
                }
            };

            var orderDto = Mapper.Map<OrderDto>(order);
            Assert.AreEqual(DataConstants.OrderStatus.Cancelled, orderDto.CalculatedStatusId);
        }
    }
}
