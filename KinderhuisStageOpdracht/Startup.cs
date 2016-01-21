using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KinderhuisStageOpdracht.Startup))]
namespace KinderhuisStageOpdracht
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
