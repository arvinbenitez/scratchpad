using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class DeliveryReceiptMapperConfiguration
    {
        public static void ConfigureDeliveryReceipt()
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
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<DeliveryReceiptDetail>, DeliveryReceiptDetail, DeliveryDto>
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
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<DeliveryReceiptDetail>, DeliveryReceiptDetail, DeliveryDto>
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
    }
}