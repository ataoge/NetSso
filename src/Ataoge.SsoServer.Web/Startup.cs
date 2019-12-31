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
using Ataoge.SsoServer.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using Ataoge.SsoServer.WebSockets;
using Ataoge.SsoServer.Web.Services;
using Ataoge.SsoServer.Web.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Ataoge.SsoServer.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void StartupConfigureServices(IServiceCollection services, bool useLocal = true)
        {
            
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                options.UseIdentityServer(opts => {
                    opts.UseInnerModel = true;
                });
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddAtaogeEfStores<ApplicationDbContext>()
                .AddUserManager<LdapUserManager<ApplicationUser, int>>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, AtaogeClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

            services.AddIdentityServer(options => {
                    options.UserInteraction.ConsentUrl = new PathString("/Identity/Consent");
                    options.UserInteraction.DeviceVerificationUrl = new PathString("/Identity/Device");
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryPersistedGrants()
                //.AddOperationalStore<ApplicationDbContext>()
                .AddConfigurationStore<ApplicationDbContext>()
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryApiResources(Config.GetApis())
                //.AddInMemoryClients(Config.GetClients())
                .AddProfileService<IdentityServer4.AspNetIdentity.ProfileService<ApplicationUser>>();

            
            services.AddControllersWithViews();
            services.AddRazorPages();

            // 权限相关
            services.AddSingleton<IAuthorizationHandler, OnlineUserManageHandler>();

            // 在线用户缓存、更新相关
            services.AddSingleton<IOnlineUserService, MemoryOnlineUserService>();
            services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, OnlineUserUpdateService>();

            services.AddWebSocketManager();
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

            // 代理服务器
            var forwardedHeadersOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();
            forwardedHeadersOptions.KnownProxies.Add(IPAddress.Parse("172.17.0.1"));
            forwardedHeadersOptions.KnownProxies.Add(IPAddress.Parse("192.168.200.55"));
            app.UseForwardedHeaders(forwardedHeadersOptions);
            app.UseBasicForwardedHeaders(new BasicForwardedHeadersOptions(){
                ForwardedHeaders = AspNetCore.BasicOverrides.BasicForwardedHeaders.XForwardedPathBase | AspNetCore.BasicOverrides.BasicForwardedHeaders.IntranetPenetration
            });

            app.UseRouting();

            app.UseWebSockets();
            app.MapWebSocketManager("/ws", app.ApplicationServices.GetRequiredService<LoginMessageHandler>());

            app.UseIdentityServer(); //IdentityServer4
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
