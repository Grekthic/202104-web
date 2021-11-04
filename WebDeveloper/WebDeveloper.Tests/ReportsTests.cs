using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class ReportsTests
    {
        [Fact]
        public void ElReportTopNDebeFuncionar()
        {
            // Preparar la prueba
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            // Ejecutar el codigo
            var reportsService = new ReportsService(context);
            var resultado = reportsService.ObtenerTopDeCancionesVendidas(3);

            // Comprobacion (Assert)
            Assert.True(resultado.Count == 3);
        }
    }
}
