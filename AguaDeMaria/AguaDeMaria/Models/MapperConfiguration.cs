using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Delivery;
using AguaDeMaria.Models.Order;
using AguaDeMaria.Models.Pickup;
using AguaDeMaria.Models.Invoice;
using AutoMapper;

namespace AguaDeMaria.Models
{
    public class MapperConfiguration
    {
        public static void Configure()
        {
            ConfigureOrder();
            ConfigureDeliveryReceipt();
            ConfigurePickupSlip();
            ConfigureSalesInvoice();
        }

        private static void ConfigureOrder()
        {
            Mapper.CreateMap<Model.Order, OrderDto>()
                .ForMember(x => x.SlimQty,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Qty))
                .ForMember(x => x.SlimOrderDetailId,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).OrderDetailId))
                .ForMember(x => x.RoundQty,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Qty))
                .ForMember(x => x.RoundOrderDetailId,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).OrderDetailId))
                .ForMember(x => x.CustomerName,
                    o => o.MapFrom(s => s.Customer.CustomerName))
                .ForMember(x => x.OrderStatusName,
                    o => o.MapFrom(s => s.OrderStatus.StatusName));

            Mapper.CreateMap<OrderDto, Model.Order>()
                .AfterMap((x, y) => MapChildToCollection<ICollection<OrderDetail>, OrderDetail, OrderDto>(y.OrderDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Round,
                    (a, b) =>
                    {
                        a.OrderDetailId = b.RoundOrderDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Round;
                        a.Qty = b.RoundQty;
                    })
                )
                .AfterMap((x, y) => MapChildToCollection<ICollection<OrderDetail>, OrderDetail, OrderDto>(y.OrderDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Slim,
                    (a, b) =>
                    {
                        a.OrderDetailId = b.SlimOrderDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Slim;
                        a.Qty = b.SlimQty;
                    })
                );
        }

        public static void ConfigureSalesInvoice()
        {
            Mapper.CreateMap<SalesInvoice, SalesInvoiceDto>()
                .ForMember(x => x.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName))
                .ForMember(x => x.OrderNumber, o => o.MapFrom(s => s.Order.OrderNumber));
        }

        private static void ConfigureDeliveryReceipt()
        {
            //Map to Order to DeliveryReceipt
            Mapper.CreateMap<Model.Order, DeliveryDto>()
                .ForMember(x => x.SlimQty,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Qty))
                .ForMember(x => x.RoundQty,
                    o => o.MapFrom(s => s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Qty))
                .ForMember(x => x.CustomerName,
                    o => o.MapFrom(s => s.Customer.CustomerName));

            Mapper.CreateMap<Model.DeliveryReceipt, DeliveryDto>()
                .ForMember(x => x.SlimQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Quantity))
                .ForMember(x => x.SlimDeliveryReceiptDetailId,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).DeliveryReceiptDetailId))
                .ForMember(x => x.SlimAmount,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Amount))
                .ForMember(x => x.SlimUnitPrice,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).UnitPrice))
                .ForMember(x => x.RoundQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Quantity))
                .ForMember(x => x.RoundDeliveryReceiptDetailId,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).DeliveryReceiptDetailId))
                .ForMember(x => x.RoundAmount,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Amount))
                .ForMember(x => x.RoundUnitPrice,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).UnitPrice))
                .ForMember(x => x.CustomerName,
                    o => o.MapFrom(s => s.Customer.CustomerName))
                .ForMember(x => x.OrderNumber,
                    o => o.MapFrom(s => s.Order.OrderNumber))
                .ForMember(x => x.CustomerAddress,
                    o => o.MapFrom(s => s.Customer.Address));

            //Map DeliveryDto to DeliveryReceipt
            Mapper.CreateMap<DeliveryDto, Model.DeliveryReceipt>()
                .AfterMap((x, y) => MapChildToCollection<ICollection<DeliveryReceiptDetail>, DeliveryReceiptDetail, DeliveryDto>
                            (y.DeliveryReceiptDetails, x, z => z.ProductTypeId == DataConstants.ProductTypes.Slim,
                            (a, b) =>
                            {
                                a.DeliveryReceiptDetailId = b.SlimDeliveryReceiptDetailId;
                                a.ProductTypeId = DataConstants.ProductTypes.Slim;
                                a.Quantity = b.SlimQty;
                                a.UnitPrice = b.SlimUnitPrice;
                                a.Amount = b.SlimAmount;

                            })
                )
                .AfterMap((x, y) => MapChildToCollection<ICollection<DeliveryReceiptDetail>, DeliveryReceiptDetail, DeliveryDto>
                            (y.DeliveryReceiptDetails, x, z => z.ProductTypeId == DataConstants.ProductTypes.Round,
                            (a, b) =>
                            {
                                a.DeliveryReceiptDetailId = b.RoundDeliveryReceiptDetailId;
                                a.ProductTypeId = DataConstants.ProductTypes.Round;
                                a.Quantity = b.RoundQty;
                                a.UnitPrice = b.RoundUnitPrice;
                                a.Amount = b.RoundAmount;
                            })
                );
        }

        public static void ConfigurePickupSlip()
        {
            //Map to Order to DeliveryReceipt
            Mapper.CreateMap<Model.DeliveryReceipt, PickupSlipDto>()
                .ForMember(x => x.SlimQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Quantity))
                .ForMember(x => x.RoundQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Quantity))
                .ForMember(x => x.CustomerName,
                    o => o.MapFrom(s => s.Customer.CustomerName));

            Mapper.CreateMap<Model.PickupSlip, PickupSlipDto>()
                    .ForMember(x => x.SlimQty,
                        o => o.MapFrom(s => s.PickupSlipDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Quantity))
                    .ForMember(x => x.SlimPickupSlipDetailId,
                        o => o.MapFrom(s => s.PickupSlipDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).PickupSlipDetailId))
                    .ForMember(x => x.RoundQty,
                        o => o.MapFrom(s => s.PickupSlipDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Quantity))
                    .ForMember(x => x.RoundPickupSlipDetailId,
                        o => o.MapFrom(s => s.PickupSlipDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).PickupSlipDetailId))
                    .ForMember(x => x.CustomerName,
                        o => o.MapFrom(s => s.Customer.CustomerName));

            Mapper.CreateMap<PickupSlipDto, Model.PickupSlip>()
                .AfterMap((x, y) => MapChildToCollection<ICollection<PickupSlipDetail>, PickupSlipDetail, PickupSlipDto>(y.PickupSlipDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Round,
                    (a, b) =>
                    {
                        a.PickupSlipDetailId = b.RoundPickupSlipDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Round;
                        a.Quantity = b.RoundQty;
                    })
                )
                .AfterMap((x, y) => MapChildToCollection<ICollection<PickupSlipDetail>, PickupSlipDetail, PickupSlipDto>(y.PickupSlipDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Slim,
                    (a, b) =>
                    {
                        a.PickupSlipDetailId = b.SlimPickupSlipDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Slim;
                        a.Quantity = b.SlimQty;
                    })
                );

        }


        #region Common Functions
        private static void MapChildToCollection<TCollection, TChild, TSource>(TCollection collectionObject,
                                                           TSource sourceObject,
                                                           Func<TChild, bool> existenceCheckPredicate,
                                                           Action<TChild, TSource> mappingPredicate)
            where TCollection : ICollection<TChild>
            where TChild : class , new()
        {
            var child = collectionObject.FirstOrDefault(existenceCheckPredicate);
            if (child == null)
            {
                child = new TChild();
                collectionObject.Add(child);
            }
            mappingPredicate.Invoke(child, sourceObject);
        }
        #endregion
    }
}