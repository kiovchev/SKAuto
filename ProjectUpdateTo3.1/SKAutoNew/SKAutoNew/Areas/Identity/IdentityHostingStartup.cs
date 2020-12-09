using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SKAutoNew.Areas.Identity.IdentityHostingStartup))]
namespace SKAutoNew.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}