using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(valorantApplication.Startup))]
namespace valorantApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
