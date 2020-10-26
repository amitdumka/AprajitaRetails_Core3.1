using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AprajitaRetails.Areas.Identity.IdentityHostingStartup))]
namespace AprajitaRetails.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}