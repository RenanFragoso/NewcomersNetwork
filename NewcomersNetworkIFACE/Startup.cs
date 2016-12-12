using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewcomersNetworkIFACE.Startup))]
namespace NewcomersNetworkIFACE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
