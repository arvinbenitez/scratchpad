using AguaDeMaria.Model;
using AguaDeMaria.Models.Invoice;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class SalesInvoiceMapperConfiguration
    {
        public static void ConfigureSalesInvoice()
        {
            Mapper.CreateMap<SalesInvoice, SalesInvoiceDto>()
                .ForMember(x => x.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName));
        }
    }
}