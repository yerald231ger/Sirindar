using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sirindar.Startup))]
namespace Sirindar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            ConfigureAuth(app);
        }
    }
}
