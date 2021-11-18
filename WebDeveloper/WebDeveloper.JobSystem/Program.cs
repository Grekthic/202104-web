using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.JobSystem
{
    class Program
    {
        async static Task Main(string[] args)
        {
            // Crear e inicializar la conexion al hub
            Console.WriteLine("Inicializando conexion con el hub");
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5003/trackshub")
                .Build();
            await hubConnection.StartAsync();
            Console.WriteLine("Se inicializo la conexion satisfactoriamente!");
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);
            while (true)
            {
                // Esperar 3 segundos entre consulta
                System.Threading.Thread.Sleep(3000);
                // Consultar la cantidad de tracks en la BD
                var count = await context.Tracks.CountAsync();
                Console.WriteLine($"La cantidad de canciones es: {count}");

                // Invocar el metodo del TracksHub
                await hubConnection.InvokeAsync("NuevaConsultaCantidadCanciones", count);
            }
        }
    }
}
