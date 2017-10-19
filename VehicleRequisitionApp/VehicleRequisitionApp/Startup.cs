using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleRequisitionApp.Startup))]
namespace VehicleRequisitionApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
