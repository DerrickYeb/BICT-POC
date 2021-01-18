using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BICT_POC.Startup))]
namespace BICT_POC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
