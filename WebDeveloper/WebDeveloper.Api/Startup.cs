using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.Api
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
            // Configurar el esquema de autenticacion basado en JWTs
            // AddJwtBearer significa que los clientes van a tener que enviar el JWT en 
            // el header de la solicitude de la siguiente forma:
            // Authorization: Bearer jwt_token
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtConfig =>
            {
                // Lo ideal es que este flag este habilitado para produccion
                jwtConfig.RequireHttpsMetadata = false;
                jwtConfig.TokenValidationParameters = new TokenValidationParameters
                {
                    // El Issuer es la entidad que genero el token
                    ValidIssuer = "Cibertec",
                    // Los clientes (audiences) que estan permitidos a usar estos JWTs
                    ValidAudience = "app-react",
                    // La llave privada que se usara para firmar los JWTs
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mi-llave-ultra-secreta")),
                    // Validar la expiracion del JWT
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
            });
            // Inyectar la dependencia hacia el db context
            services.AddDbContext<IChinookContext, ChinookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ChinookConnection")));
            
            
            services.AddControllers(config=>
            {
                // Con esto estamos haciendo que todas las acciones (endpoints) se autentiquen
                config.Filters.Add(new AuthorizeFilter());
            })
                // Configurar el serializador de JSON (Por defecto se usa System.Text.Json)
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Configurar Swagger
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chinook API",
                    Description = "Especificacion de los Servicios REST de Chinook",
                    TermsOfService = new Uri("https://algo.com"),
                    Contact = new OpenApiContact
                    {
                        Email = "correo@mail.com",
                        Name = "Arturo Balbin",
                    }
                });

                // Configuracion para leer el erchivo XML de documentacion de codigo
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Para logear todas las solicitudes a cualquier accion de cualquier controlador
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            // Activar el uso de Swagger en el proyecto
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                config.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
