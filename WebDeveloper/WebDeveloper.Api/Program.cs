using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebDeveloper.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Niveles de log: debug -> info -> warning -> error -> fatal
            // Leer el appsettings.json para obtener la configuracion del logger
            var loggerConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Inicializar el logger de Serilog con la configuracion anterior
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(loggerConfiguration)
                .CreateLogger();

            try
            {
                Log.Information("Inicializando web host");
                //throw new Exception("Error forzado");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Hubo un error al inicializar el servicio");
            }
            finally
            {
                // Limpiar el objeto Log
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
