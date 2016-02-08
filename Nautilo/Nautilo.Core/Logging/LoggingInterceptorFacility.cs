using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;

namespace Nautilo.Core.Logging
{
    public class LoggingInterceptorFacility: AbstractFacility
    {
        protected override void Init()
        {
            Kernel.ComponentRegistered += KernelOnComponentRegistered;
        }

        private void KernelOnComponentRegistered(string key, IHandler handler)
        {
            if (typeof (ILogMe).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof (LoggingInterceptor)));
            }
        }
    }
}