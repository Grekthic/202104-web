using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDeveloper.CibertecMvcIdentity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace WebDeveloper.CibertecMvcIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar nuestro DBContext para el acceso a datos
            services.AddDbContext<ChinookIdentityContext>(options => options.UseSqlServer("server=.;database=chinook_identity;trusted_connection=true"));

            // Configurar ASP Net Identity
            services.AddIdentity<ChinookUser, IdentityRole>(config =>
            {
                // Politicas de la contrasenia
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                // Solo usuarios con el email confirmado pueden iniciar sesion
                config.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ChinookIdentityContext>();

            // Configuracion adicional de Identity
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Cuentas/IniciaSesion";
            });

            // COnfigurar el envio de correos con mailkit
            services.AddMailKit(options =>
            {
                options.UseMailKit(new MailKitOptions
                {
                    Server = "127.0.0.1",
                    Port = 25,
                    SenderEmail = "soporte@chinook.com",
                    SenderName = "Soporte Chinook"
                });
            });

            // Configurar MVC
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hello-world", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                // El mapeo de ruta por defecto de MVC
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
