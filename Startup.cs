using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(final_version2.Startup))]
namespace final_version2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
