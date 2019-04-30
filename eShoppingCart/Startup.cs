using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eShoppingCart.Startup))]
namespace eShoppingCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
