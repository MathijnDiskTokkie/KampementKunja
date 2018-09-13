using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wagenpark.Startup))]
namespace Wagenpark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
