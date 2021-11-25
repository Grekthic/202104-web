using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Requests.Account;

namespace WebDeveloper.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IChinookContext _chinookContext;

        public AccountController(IChinookContext chinookContext)
        {
            _chinookContext = chinookContext;
        }

        [HttpPost("token-test")]
        [AllowAnonymous] // Hacemos que esta ruta sea publica
        public IActionResult GetTestToken([FromBody]GetTestTokenRequest request)
        {
            // Crear los objetos para firmar el JWT
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mi-llave-ultra-secreta"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            // Crear el JWT
            var tokenParams = new JwtSecurityToken(
                    issuer: "Cibertec",
                    audience: request.Audience,
                    expires: DateTime.Now.AddSeconds(30 * 60),
                    signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenParams);

            return Ok(new { token }); // JSON -> { token: "valor del token" }
        }

        [HttpPost("token")]
        [AllowAnonymous] // Hacemos que esta ruta sea publica
        public async Task<IActionResult> GetToken([FromBody]GetTokenRequest request)
        {
            // Validacion del request
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el usuario de la BD
            var user = await _chinookContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            // Si no se encontro el usuario
            if(user == null)
            {
                return Unauthorized(new { message = "Credenciales invalidas" });
            }

            // Construir la identidad del usuario basada en claims
            var identityClaims = new[]
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("dni", user.Dni)
            };

            // Crear los objetos para firmar el JWT
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mi-llave-ultra-secreta"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Crear el JWT
            var expirationDate = DateTime.Now.AddSeconds(30 * 60);
            var tokenParams = new JwtSecurityToken(
                    issuer: "Cibertec",
                    audience: "app-react",
                    expires: expirationDate,
                    signingCredentials: credentials,
                    claims: identityClaims
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenParams);

            return Ok(new { token, expires = new DateTimeOffset(expirationDate).ToUnixTimeSeconds() });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(User.Identity);
        }
    }
}
