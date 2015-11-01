using System.ComponentModel;
using Container = Funq.Container;

namespace AguaDeMaria.Configuration.Ioc
{
    public interface IServiceResolver
    {
        T Resolve<T>();
        T Resolve<T>(string name);
    }

    public class ServiceResolver : IServiceResolver
    {
        private readonly Container container;

        public ServiceResolver(Container container)
        {
            this.container = container;
        }

        public static ServiceResolver Instance { get; private set; }
        public static void Initialize(Container container)
        {
            Instance = new ServiceResolver(container);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return container.ResolveNamed<T>(name);
        }
    }
}