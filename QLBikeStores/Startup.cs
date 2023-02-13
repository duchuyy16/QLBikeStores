using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QLBikeStores.Areas.Admin.Services;
using QLBikeStores.Areas.Admin.Services.Impl;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QLBikeStores
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
            FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            services.AddControllersWithViews();
            //services.AddDbContext<demoContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            //services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    option.LoginPath = "/Account/Login";
                    option.AccessDeniedPath = "/Account/Login";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Google:AppId"];
                    options.ClientSecret = Configuration["Google:AppSecret"];
                    //options.ClaimActions.MapJsonKey("Picture", "picture", "url");
                    options.SaveTokens = true;
                    options.CallbackPath = Configuration["Google:CallbackPath"];
                });
            services.AddHttpContextAccessor();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                var method = context.HttpContext.Request.Method;

                if (!method.ToUpper().Equals("GET")) return Task.CompletedTask;

                switch (response.StatusCode)
                {
                    case (int)HttpStatusCode.NotFound:
                        response.Redirect("/Common/PageNotFound");
                        break;
                    case (int)HttpStatusCode.Unauthorized:
                        response.Redirect("/Account/Login");
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        response.Redirect("/Common/NoPermission");
                        break;
                    case (int)HttpStatusCode.BadRequest:
                        response.Redirect("/Common/PageBadRequest");
                        break;
                }

                return Task.CompletedTask;
            });

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
            app.UseFastReport();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "MyAreaAdmin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Accounts}/{action=Login}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
