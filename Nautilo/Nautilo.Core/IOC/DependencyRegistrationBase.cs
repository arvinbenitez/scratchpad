using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nautilo.Core.Logging;

namespace Nautilo.Core.IOC
{
    public abstract class DependencyRegistrationBase
    {
        private readonly IWindsorContainer container;

        protected DependencyRegistrationBase(IWindsorContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            RegisterLogging();
            RegisterDependencies(container);
        }

        protected abstract void RegisterDependencies(IWindsorContainer container);

        private void RegisterLogging()
        {
            container.Register(Component.For<LoggingInterceptor>().ImplementedBy<LoggingInterceptor>().LifestyleTransient());
            container.AddFacility<LoggingInterceptorFacility>();
        }
    }
}