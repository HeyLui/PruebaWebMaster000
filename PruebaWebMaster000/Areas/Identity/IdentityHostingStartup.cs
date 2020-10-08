using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaWebMaster000.Data;

[assembly: HostingStartup(typeof(PruebaWebMaster000.Areas.Identity.IdentityHostingStartup))]
namespace PruebaWebMaster000.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PruebaWebMasterContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PruebaWebMasterContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => {

                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<PruebaWebMasterContext>();
            });
        }
    }
}