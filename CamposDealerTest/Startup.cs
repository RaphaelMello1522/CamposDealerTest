using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CamposDealerTest.Startup))]
namespace CamposDealerTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
