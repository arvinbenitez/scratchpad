using System.Web.Mvc;
using AguaDeMaria.Configuration.Ioc;
using AguaDeMaria.Configuration.Mappers;
using Funq;
using ServiceStack.Common.Web;
using ServiceStack.Mvc;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace AguaDeMaria
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Aqua Kesh Web services", typeof (AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //ASP.NET MVC integration
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            SetConfig(CreateEndpointHostConfig());


            JsConfig.EmitCamelCaseNames = true;

            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof (AppHost).Assembly);

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {new CredentialsAuthProvider()}
                ));
            WebBootStrapper.ConfigureDependencies(container);
            ConfigureAuthorization(container);

            MapperConfiguration.Configure();

            ServiceResolver.Initialize(container);
        }

        private static void ConfigureAuthorization(Container container)
        {
            var userAuthRepository = (OrmLiteAuthRepository) container.Resolve<IUserAuthRepository>();
#if DEBUG
            userAuthRepository.CreateMissingTables();
#endif
            if (userAuthRepository.GetUserAuthByUserName("Administrator") == null)
            {
                userAuthRepository.CreateUserAuth(
                    new UserAuth
                    {
                        Email = "admin@getsomething.com",
                        UserName = "Administrator",
                        DisplayName = "Admin User"
                    }, "P@55word");
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