using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Delivery;
using AguaDeMaria.Models.Order;
using AutoMapper;

namespace AguaDeMaria.Models
{
    public class MapperConfiguration
    {
        public static void Configure()
        {
            ConfigureOrder();
            ConfigureDeliveryReceipt();
        }

        private static void ConfigureOrder()
        {
            Mapper.CreateMap<Model.Order, OrderDto>()
                .ForMember(x => x.SlimQty,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim)
                                    .Qty))
                .ForMember(x => x.SlimOrderDetailId,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim)
                                    .OrderDetailId))
                .ForMember(x => x.RoundQty,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round)
                                    .Qty))
                .ForMember(x => x.RoundOrderDetailId,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round)
                                    .OrderDetailId))
                .ForMember(x => x.CustomerName,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Customer.CustomerName))
                .ForMember(x => x.OrderStatusName,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderStatus.StatusName));


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

        private static void ConfigureDeliveryReceipt()
        {
            //Map to Order to DeliveryReceipt
            Mapper.CreateMap<Model.Order, DeliveryDto>()
                .ForMember(x => x.SlimQty,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim)
                                    .Qty))
                .ForMember(x => x.RoundQty,
                    o =>
                        o.MapFrom(
                            s =>
                                s.OrderDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round)
                                    .Qty))
                .ForMember(x => x.CustomerName,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Customer.CustomerName));

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
    }
}