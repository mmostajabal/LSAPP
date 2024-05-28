using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LSAPP.Startup))]
namespace LSAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
