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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie()
           .AddGoogle(options =>
            {
                options.ClientId = "996931337200-ptgdnpmn4tcf8sf7e5fuaetdnenhvtgs.apps.googleusercontent.com";
                options.ClientSecret = "GzdaBo_AVYuwY5hk2RQRvu8d";
                options.SaveTokens = true;
                options.Events.OnRemoteFailure = (context) =>
                {
                    context.HandleResponse();
                    return context.Response.WriteAsync("<script>window.close();</script>");
                };
            });
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
                endpoints.MapControllers();
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
                "If the Easter Bunny and the Tooth Fairy had babies would they take your teeth and leave chocolate for you?",
                "There was no ice cream in the freezer, nor did they have money to go to the store."
            };

            _context.Add(entity1);
            _context.SaveChanges();


            var entity2 = new eThorTestEntity()
            {
                Name = "entity 2"
            };
            entity2.HardPropertyList = new List<string>() {
                "The old apple revels in its authority.",
                "Writing a list of random sentences is harder than I initially thought it would be.",
                "I am counting my calories, yet I really want dessert.",
                "I am never at home on Sundays."

            };
            _context.Add(entity2);
            _context.SaveChanges();


            var entity3 = new eThorTestEntity()
            {
                Name = "entity 3"
            };
            entity3.HardPropertyList = new List<string>() {
                "The clock within this blog and the clock on my laptop are 1 hour different from each other.",
                "The mysterious diary records the voice.",
                "Malls are great places to shop; I can find everything I need under one roof.",
                "I was very proud of my nickname throughout high school but today- I couldn’t be any different to what my nickname was."

            };
            _context.Add(entity3);
            _context.SaveChanges();
        }
    }

}
