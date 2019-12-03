using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ataoge.SsoServer.OpenIdClient
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; //"oidc";
            })
          .AddCookie("Cookies")
          .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
          {
              options.SignInScheme = "Cookies";
              options.Authority = "https://localhost:5001";
              options.RequireHttpsMetadata = false;
              options.ClientId = "server.code";
              options.ClientSecret = "secret";
              options.ResponseType = "code id_token";
              options.GetClaimsFromUserInfoEndpoint = true;
              options.Scope.Add("api");
              options.Scope.Add("offline_access");
                options.SaveTokens = true;

                options.Events.OnRedirectToIdentityProvider = context => { 
                    //context.ProtocolMessage.AcrValues = "idp:Microsoft tenant:1";
                    return Task.CompletedTask;
                };
                
               options.ClaimActions.MapUniqueJsonKey(System.Security.Claims.ClaimTypes.Name,"displayname");
               options.ClaimActions.MapUniqueJsonKey("displayname", "displayname");
                        options.ClaimActions.MapUniqueJsonKey("preferred_username","preferred_username");
                        options.ClaimActions.MapUniqueJsonKey("picture", "picture");
          });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
