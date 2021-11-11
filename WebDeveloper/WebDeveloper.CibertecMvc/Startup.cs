using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.CibertecMvc
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
            // Configurar el esquema de autenticacion
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "CibertecAuth";
                })
                .AddGoogle(config =>
                {
                    config.ClientId = "";
                    config.ClientSecret = "";
                    config.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                    config.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
                    config.Events.OnTicketReceived = googleContext =>
                    {
                        var nameIdentifier = googleContext.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

                        // El name identifier deberia ser el email de google
                        // Con esto podriamos validar si el usuario existe en nuestra BD
                        // Si no exstiese, podriamos crear un nuevo usuario
                        return Task.CompletedTask;
                    };
                });
            // Configurar el servicio del ChinookContext
            services.AddDbContext<IChinookContext, ChinookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ChinookConnection")));
            // Configurar otras dependencias
            services.AddTransient<IReportsService, ReportsService>();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Ejemplos de rutas
                // 1. / -> HomeController -> Index
                // 2. /Artists -> ArtistsController -> Index
                // 3. /Artists/Details/1 -> ArtistsController -> Details(id)
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
