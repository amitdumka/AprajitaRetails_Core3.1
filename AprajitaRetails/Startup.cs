using Microsoft.AspNetCore.Authorization;
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
//using AprajitaRetails.Areas.Chat.Models.Hubs;
using Microsoft.AspNetCore.Http;
using AprajitaRetails.Ops.Bot.Telegram;
using AprajitaRetails.Ops.Service;
using AprajitaRetails.Ops.Bot.TelgramService;

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
            services.Configure<CookiePolicyOptions> (options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext> (options =>
                 options.UseSqlServer (
                     Configuration.GetConnectionString ("DefaultConnection")));

            services.AddDbContext<AprajitaRetailsContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("AprajitaRetailsConnection")));

            services.AddDbContext<VoyagerContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("VoyagerConnection")));
            services.AddDbContext<VoyagerContext> (options =>
               options.UseSqlServer (
                   Configuration.GetConnectionString ("AccountsConnection")));

            services.AddDefaultIdentity<IdentityUser> (options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            // services.AddSignalR ();

            services.AddControllersWithViews ();
            services.AddRazorPages ();

            services.AddMvc ().AddViewLocalization (LanguageViewLocationExpanderFormat.Suffix);
            services.AddPortableObjectLocalization ();

            services.Configure<RequestLocalizationOptions> (options =>
             {
                 var supportedCultures = new List<CultureInfo>
             {
                new CultureInfo("en-IN"),
                new CultureInfo("en"),
                new CultureInfo("hi-IN"),
                new CultureInfo("hi")
             };

                 options.DefaultRequestCulture = new RequestCulture ("en-IN");
                 options.SupportedCultures = supportedCultures;
                 options.SupportedUICultures = supportedCultures;
             });
            services.AddApplicationInsightsTelemetry ();

            //services.AddScoped<IUpdateService, UpdateService> ();
           services.AddSingleton<IGiniService, GiniService> ();
            //services.AddSingleton<IGiniService> ( sp=> new GiniService());
            //services.Configure<BotConfiguration> (Configuration.GetSection ("BotConfiguration"));

            services.AddControllers ().AddNewtonsoftJson ();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if ( env.IsDevelopment () )
            {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            }
            else
            {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseStaticFiles ();
            app.UseCookiePolicy ();
            app.UseRouting ();
            app.UseAuthentication ();
            app.UseAuthorization ();
            app.UseRequestLocalization ();

            app.UseEndpoints (endpoints =>
             {
                 endpoints.MapControllerRoute (
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                 endpoints.MapControllerRoute (
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
                 endpoints.MapRazorPages ();
             });


        }
    }
}
