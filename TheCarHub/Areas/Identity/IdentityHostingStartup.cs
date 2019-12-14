using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TheCarHub.Areas.Identity.IdentityHostingStartup))]
namespace TheCarHub.Areas.Identity
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