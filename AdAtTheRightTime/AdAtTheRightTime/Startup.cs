using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdAtTheRightTime.Startup))]
namespace AdAtTheRightTime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
