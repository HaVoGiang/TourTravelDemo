using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourTravelDemo.Startup))]
namespace TourTravelDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
