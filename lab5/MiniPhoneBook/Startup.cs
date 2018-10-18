using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniPhoneBook.Startup))]
namespace MiniPhoneBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
