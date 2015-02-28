using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Pickup;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class PickupSlipMapperConfiguration
    {
        public static void ConfigurePickupSlip()
        {
            //Map to Order to DeliveryReceipt
            Mapper.CreateMap<DeliveryReceipt, PickupSlipDto>()
                .ForMember(x => x.SlimQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Slim).Quantity))
                .ForMember(x => x.RoundQty,
                    o => o.MapFrom(s => s.DeliveryReceiptDetails.FirstOrDefault(n => n.ProductTypeId == DataConstants.ProductTypes.Round).Quantity))
                .ForMember(x => x.CustomerName,
                    o => o.MapFrom(s => s.Customer.CustomerName));

            Mapper.CreateMap<PickupSlip, PickupSlipDto>()
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

            Mapper.CreateMap<PickupSlipDto, PickupSlip>()
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<PickupSlipDetail>, PickupSlipDetail, PickupSlipDto>(y.PickupSlipDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Round,
                    (a, b) =>
                    {
                        a.PickupSlipDetailId = b.RoundPickupSlipDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Round;
                        a.Quantity = b.RoundQty;
                    })
                )
                .AfterMap((x, y) => MapperConfiguration.MapChildToCollection<ICollection<PickupSlipDetail>, PickupSlipDetail, PickupSlipDto>(y.PickupSlipDetails, x,
                    z => z.ProductTypeId == DataConstants.ProductTypes.Slim,
                    (a, b) =>
                    {
                        a.PickupSlipDetailId = b.SlimPickupSlipDetailId;
                        a.ProductTypeId = DataConstants.ProductTypes.Slim;
                        a.Quantity = b.SlimQty;
                    })
                );

        }
    }
}