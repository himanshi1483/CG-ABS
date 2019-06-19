using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABS_v1.Startup))]
namespace ABS_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
