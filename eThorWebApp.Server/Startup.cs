using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System;
using eThorWebApp.Shared.Interfaces;
using eThorWebApp.Shared.Services;
using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace eThorWebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var windsorContainer = new WindsorContainer();
            services.AddSingleton<IethorService, ethorService>();
            services.AddDbContext<eThorTestEntityContext>(opt => opt.UseInMemoryDatabase("eThorTestEntityDb"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddOpenIdConnect(
            authenticationScheme: "Google",
            displayName: "Google",
            options =>
            {
                options.Authority = "https://accounts.google.com/o/oauth2/auth";
                options.MetadataAddress = "https://accounts.google.com/.well-known/openid-configuration";
                options.ClientId = "354113905172-lbkcvqvareshr1p3kgug4ev27eh72k8q.apps.googleusercontent.com";
                options.ClientSecret = "KMMNmREmVy0jwZajf3bWolIW";
                options.SaveTokens = true;
            });
            services.AddAuthorizationCore();
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(windsorContainer, services);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseClientSideBlazorFiles<Client.Startup>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            var context = app.ApplicationServices.GetService<eThorTestEntityContext>();
            SeedData(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }

        private static void SeedData(eThorTestEntityContext _context)
        {
            var entity1 = new eThorTestEntity()
            {
                Name = "entity 1",

            };
            entity1.HardPropertyList = new List<string>() {
                "list of text 1 ",
                "list of text 2 "
            };

            _context.Add(entity1);
            _context.SaveChanges();


            var entity2 = new eThorTestEntity()
            {
                Name = "entity 2"
            };
            entity2.HardPropertyList = new List<string>() {
                "list of text 3 ",
                "list of text 4 "
            };
            _context.Add(entity2);
            _context.SaveChanges();

        }
    }

}
