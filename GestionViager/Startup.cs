using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionViager.Startup))]
namespace GestionViager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
