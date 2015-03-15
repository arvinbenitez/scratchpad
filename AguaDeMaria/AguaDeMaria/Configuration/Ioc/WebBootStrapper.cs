using System.Configuration;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Report;
using AguaDeMaria.Service;
using AguaDeMaria.Service.Implementation;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface.Auth;

namespace AguaDeMaria.Configuration.Ioc
{
    public class WebBootStrapper
    {
        /// <summary>
        ///     Setup Dependency Injection
        /// </summary>
        /// <param name="container"></param>
        public static void ConfigureDependencies(Container container)
        {
            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(
                    ConfigurationManager.ConnectionStrings["SQLAuthorization"].ConnectionString,
                    SqlServerDialect.Provider));
            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            container.RegisterAutoWired<AguaDeMariaContext>().ReusedWithin(ReuseScope.Request);
            //Unit of Work
            container.RegisterAutoWiredAs<UnitOfWork, IUnitOfWork>().ReusedWithin(ReuseScope.Request);

            //Model Repository Dependencies
            container.RegisterAutoWiredAs<GenericRepository<Customer>, IRepository<Customer>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<Order>, IRepository<Order>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<DeliveryReceipt>, IRepository<DeliveryReceipt>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<PickupSlip>, IRepository<PickupSlip>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<Inventory>, IRepository<Inventory>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<InventorySummary>, IRepository<InventorySummary>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<DeliveryReceiptLedger>, IRepository<DeliveryReceiptLedger>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<Receivable>, IRepository<Receivable>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<SalesInvoice>, IRepository<SalesInvoice>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<SalesInvoiceDetail>, IRepository<SalesInvoiceDetail>>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<DailySummary>, IRepository<DailySummary>>()
                            .ReusedWithin(ReuseScope.Request);

            //Services
            container.RegisterAutoWiredAs<DeliveryReceiptService, IDeliveryReceiptService>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<OrderService, IOrderService>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<DeliveryReceiptLedgerService, IDeliveryReceiptLedgerService>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<PaymentService, IPaymentService>()
                .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<ReportService, IReportService>()
                .ReusedWithin(ReuseScope.Request);

            //Lookup Dependencies
            container.RegisterAutoWiredAs<GenericRepository<CustomerType>, IRepository<CustomerType>>();
            container.RegisterAutoWiredAs<GenericRepository<ProductType>, IRepository<ProductType>>();
            container.RegisterAutoWiredAs<GenericRepository<OrderStatus>, IRepository<OrderStatus>>();
            container.RegisterAutoWiredAs<GenericRepository<Setting>, IRepository<Setting>>();


            container.RegisterAutoWired<LookupDataManager>();
            container.RegisterAutoWired<SettingsManager>();
        }
    }
}