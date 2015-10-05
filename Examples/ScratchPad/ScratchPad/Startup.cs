using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScratchPad.Startup))]
namespace ScratchPad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
