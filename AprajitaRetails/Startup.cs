using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using AprajitaRetails.Areas.Voyager.Data;

using System.Net;
using System.Net.Mail;
using AprajitaRetails.Areas.Chat.Models.Hubs;

namespace AprajitaRetails
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AprajitaRetailsContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("AprajitaRetailsConnection")));
            
            services.AddDbContext<VoyagerContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("VoyagerConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSignalR ();

            //Email Service added
            //services.AddScoped<SmtpClient> ((serviceProvider) =>
            //{
            //    var config = serviceProvider.GetRequiredService<IConfiguration> ();
            //    return new SmtpClient ()
            //    {
            //        Host = config.GetValue<String> ("Email:Smtp:Host"),
            //        Port = config.GetValue<int> ("Email:Smtp:Port"),
            //        Credentials = new NetworkCredential (
            //                config.GetValue<String> ("Email:Smtp:Username"),
            //                config.GetValue<String> ("Email:Smtp:Password")
            //            )
            //    };
            //});

            //services.AddTransient<SmtpClient> ((serviceProvider) =>
            //{
            //    var config = serviceProvider.GetRequiredService<IConfiguration> ();
            //    return new SmtpClient ()
            //    {
            //        Host = config.GetValue<String> ("Email:Smtp:Host"),
            //        Port = config.GetValue<int> ("Email:Smtp:Port"),
            //        Credentials = new NetworkCredential (
            //                config.GetValue<String> ("Email:Smtp:Username"),
            //                config.GetValue<String> ("Email:Smtp:Password")
            //            )
            //    };
            //});
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddPortableObjectLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-IN"),
                new CultureInfo("en"),
                new CultureInfo("hi-IN"),
                new CultureInfo("hi")
            };

                options.DefaultRequestCulture = new RequestCulture("en-IN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();
            app.UseSignalR (route =>
            {
                route.MapHub<ChatHub> ("/Chat/ARChat/Index");

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            

        }
    }
}
