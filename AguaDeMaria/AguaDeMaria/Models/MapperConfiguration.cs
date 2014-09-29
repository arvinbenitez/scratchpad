using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Order;
using AutoMapper;

namespace AguaDeMaria.Models
{
    public class MapperConfiguration
    {
        public static void Configure()
        {
            ConfigureOrder();
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


            Action<OrderDetail, OrderDto> mapOrderDetail = (x, y) =>
            {
                x.OrderDetailId = y.RoundOrderDetailId;
                x.ProductTypeId = DataConstants.ProductTypes.Round;
                x.Qty = y.RoundQty;
            };

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

        private static void MapChildToCollection<TCollection, TChild, TSource>(TCollection collectionObject,
                                                           TSource sourceObject,
                                                           Func<TChild, bool> existenceCheckPredicate,
                                                           Action<TChild, TSource> mappingPredicate)
            where TCollection : ICollection<TChild>
            where TChild : class , new()

        {
            var child = collectionObject.FirstOrDefault(existenceCheckPredicate);
            if (child != null)
            {
                child = new TChild();
                collectionObject.Add(child);
            }
            mappingPredicate.Invoke(child, sourceObject);
        }
    }
}