using Microsoft.EntityFrameworkCore;
using System;
using WebDeveloper.Infra.Data;
using System.Linq;

namespace WebDeveloper.DbOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Probando EF Core!");

            // PASO 1: Instancia el contexto de Base de Datos
            // - Crear las opciones que tenemos que enviar al constructor
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;

            // - Instanciar el ChinookContext
            //var fallo = false;
            //try
            //{
            using (var testDispose = new TestDisposable())
            {
                using (var context = new ChinookContext(options)) // IDisposible
                {
                    // Obtener todos los artistas cuyo nombre empieza con A
                    var artistasConA = context.Artists.Where(a => a.Name.StartsWith("A"));

                    foreach (var artista in artistasConA)
                    {
                        Console.WriteLine($"El nombre del artista es: {artista.Name}");
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    // Notificar el error
            //    enviarCorreo(ex);
            //}            
        }
    }
}
