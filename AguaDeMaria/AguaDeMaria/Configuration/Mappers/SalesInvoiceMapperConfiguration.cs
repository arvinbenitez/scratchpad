using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Invoice;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class SalesInvoiceMapperConfiguration
    {
        public static void ConfigureSalesInvoice()
        {
            MapInvoiceToInvoiceDto();
            MapInvoiceToPaymentDto();
        }

        private static void MapInvoiceToInvoiceDto()
        {
            Mapper.CreateMap<SalesInvoice, SalesInvoiceDto>()
                .ForMember(x => x.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName));
        }

        private static void MapInvoiceToPaymentDto()
        {
            Mapper.CreateMap<SalesInvoice, PaymentDto>()
                .ForMember(x => x.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName));
        }
    }
}