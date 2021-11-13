using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class TrackServiceTests
    {
        [Fact]
        public async Task TodasLasCancionesDebenTenerUnNombreDeArtista()
        {
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            var service = new TrackService(context);
            var resultado = await service.ObtenerListaTracks();

            Assert.True(resultado.All(t => !string.IsNullOrEmpty(t.ArtistName)));
        }
    }
}
