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

namespace AguaDeMaria
{
    public class AppHost: AppHostBase
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

            container.Register<ICacheClient>(new MemoryCacheClient());

            Plugins.Add(new AuthFeature( () => new AuthUserSession(),
                                        new IAuthProvider[] {new CredentialsAuthProvider()}
                        ));

            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(
                    ConfigurationManager.ConnectionStrings["SQLAuthorization"].ConnectionString, SqlServerDialect.Provider));

            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));


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