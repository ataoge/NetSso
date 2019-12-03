using System;
using Ataoge.SsoServer.Web.Data;
using Ataoge.SsoServer.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Ataoge.SsoServer.Web.Areas.Identity.IdentityHostingStartup))]
namespace Ataoge.SsoServer.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 10;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.Name = ".AspNetCore.Identity.ChinaDci";
                    
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                    options.LoginPath = "/Identity/Account/Login";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.SlidingExpiration = true;

                    
                });

                services.AddAuthentication()
                    .AddMicrosoftAccount(options =>
                    {
                        //options.AuthorizationEndpoint = "http://localhost:5000/signin-oauth";
                        //options.TokenEndpoint = "http://localhost:5000/signin-oauth";
                        options.CallbackPath = "/signin-oauth";
                        options.ClientId = "79231c6a-178d-4292-8d13-67ee94c17128";//Configuration["auth:facebook:appid"];
                        options.ClientSecret = "217QEGdgex4KXMkmmCCCdYL";//Configuration["auth:facebook:appsecret"];
                    });

                //services.AddSingleton<IEmailSender, EmailSender>();
            });
        }
    }
}