using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Utils;
using ServiceStack.Common.Web;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using AguaDeMaria.Model;
using AguaDeMaria.Common.Data;

namespace AguaDeMaria
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Aqua Kesh Web services", typeof(AppHost).Assembly)
        {
        }
        public override void Configure(Funq.Container container)
        {
            //ASP.NET MVC integration
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            SetConfig(CreateEndpointHostConfig());

            JsConfig.EmitCamelCaseNames = true;

            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(AppHost).Assembly);

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                        new IAuthProvider[] { new CredentialsAuthProvider() }
                        ));
            ConfigureDependencies(container);
            ConfigureAuthorization(container);

            Models.MapperConfiguration.Configure();
        }

        private static void ConfigureAuthorization(Funq.Container container)
        {
            var userAuthRepository = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
#if DEBUG
            userAuthRepository.CreateMissingTables();
#endif
            if (userAuthRepository.GetUserAuthByUserName("admin@getsomething.com") == null)
            {
                userAuthRepository.CreateUserAuth(
                    new UserAuth { Email = "admin@getsomething.com", UserName = "Administrator", DisplayName = "Admin User" }, "P@55word");
            }
        }


        /// <summary>
        /// Setup Dependency Injection
        /// </summary>
        /// <param name="container"></param>
        private void ConfigureDependencies(Funq.Container container)
        {
            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<IDbConnectionFactory>(
                    new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["SQLAuthorization"].ConnectionString, SqlServerDialect.Provider));
            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            container.RegisterAutoWired<AguaDeMariaContext>().ReusedWithin(ReuseScope.Request);
            //Unit of Work
            container.RegisterAutoWiredAs<UnitOfWork, IUnitOfWork>().ReusedWithin(ReuseScope.Request);

            //Model Repository Dependencies
            container.RegisterAutoWiredAs<GenericRepository<Customer>, IRepository<Customer>>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<Order>, IRepository<Order>>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<DeliveryReceipt>, IRepository<DeliveryReceipt>>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<PickupSlip>, IRepository<PickupSlip>>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<Inventory>, IRepository<Inventory>>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<GenericRepository<InventorySummary>, IRepository<InventorySummary>>().ReusedWithin(ReuseScope.Request);

            //Lookup Dependencies
            container.RegisterAutoWiredAs<GenericRepository<CustomerType>, IRepository<CustomerType>>();
            container.RegisterAutoWiredAs<GenericRepository<ProductType>, IRepository<ProductType>>();
            container.RegisterAutoWiredAs<GenericRepository<OrderStatus>, IRepository<OrderStatus>>();
            container.RegisterAutoWiredAs<GenericRepository<Setting>, IRepository<Setting>>();


            container.RegisterAutoWired<LookupDataManager>();
            container.RegisterAutoWired<SettingsManager>();
        }

        protected virtual EndpointHostConfig CreateEndpointHostConfig()
        {
            return new EndpointHostConfig
            {
                DebugMode = true,
                DefaultContentType = ContentType.Json,
                ServiceStackHandlerFactoryPath = "api"
            };
        }
    }
}
