using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Order;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class OrderMapperConfiguration
    {
        public static void ConfigureOrder()
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
                    o => o.MapFrom(s => s.OrderStatus.StatusName))
                .ForMember(x=> x.SlimQtyDelivered,
                    o=> o.MapFrom(s=> GetDeliveryReceiptTotal(s, DataConstants.ProductTypes.Slim)))
                .ForMember(x=> x.RoundQtyDelivered,
                    o=> o.MapFrom(s=> GetDeliveryReceiptTotal(s, DataConstants.ProductTypes.Round)));


            Mapper.CreateMap<OrderDto, Model.Order>()
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<OrderDetail>, OrderDetail, OrderDto>(y.OrderDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Round,
                    (a, b) =>
                    {
                        a.OrderDetailId = b.RoundOrderDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Round;
                        a.Qty = b.RoundQty;
                    })
                )
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<OrderDetail>, OrderDetail, OrderDto>(y.OrderDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Slim,
                    (a, b) =>
                    {
                        a.OrderDetailId = b.SlimOrderDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Slim;
                        a.Qty = b.SlimQty;
                    })
                );
        }

        private static int GetDeliveryReceiptTotal(Model.Order order, int productTypeId)
        {
            if (order.DeliveryReceipts == null)
            {
                return 0;
            }
            return
                order.DeliveryReceipts.SelectMany(
                    deliveryReceipt =>
                        deliveryReceipt.DeliveryReceiptDetails.Where(x => x.ProductTypeId == productTypeId))
                    .Sum(deliveryReceiptDetail => deliveryReceiptDetail.Quantity);
        }
    }
}