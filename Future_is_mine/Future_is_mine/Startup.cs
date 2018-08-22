using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Future_is_mine.Startup))]
namespace Future_is_mine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
