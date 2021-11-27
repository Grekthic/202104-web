using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class JwtServiceTests
    {
        [Fact]
        public async Task DebeGenerarUnTokenRandomYGuardarEnBD()
        {
            // Preparar la prueba
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            // Ejecutar el codigo
            var jwtService = new JwtService(context, new TokenValidationParameters());
            var refreshToken = await jwtService.CrearRefreshToken(new ChinookUser
            {
                UserId = 9999
            });

            // Comprobacion (Assert)
            Assert.True(refreshToken.Token != null);
            Assert.True(refreshToken.UserId == 9999);
        }

        [Fact]
        public async Task DebeGenerarLos2TokensYValidarElJWT()
        {
            // Preparar la prueba
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            using var context = new ChinookContext(options);

            // Ejecutar el codigo
            var jwtValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "Cibertec",
                ValidAudience = "app-react",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mi-llave-ultra-secreta")),
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
            var jwtService = new JwtService(context, jwtValidationParameters);
            var crearTokenResponse = await jwtService.CrearJWT(new ChinookUser
            {
                UserId = 9999,
                FirstName = "Arturo",
                LastName = "Balbin",
                Email = "arturo@balbin.com",
                Dni = "12345678"
            });

            // Comprobacion (Assert)
            Assert.True(crearTokenResponse.Token != null);
            Assert.True(crearTokenResponse.RefreshToken != null);

            // Validar el JWT
            var userIdJWT = jwtService.ValidarJWT(crearTokenResponse.Token);

            Assert.Equal(9999, userIdJWT);
        }
    }
}
