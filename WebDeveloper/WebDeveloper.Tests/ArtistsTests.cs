using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class ArtistsTests
    {
        [Fact]
        public void LaTablaDebeTenerMasDe1Elemento()
        {
            // Preparar la prueba
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            // Ejecutar el codigo
            var artistasCount = context.Artists.Count();

            // Comprobacion (Assert)
            Assert.True(artistasCount > 1);
        }
    }
}
