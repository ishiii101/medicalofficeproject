using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapstoneProjectStart.Startup))]
namespace CapstoneProjectStart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
