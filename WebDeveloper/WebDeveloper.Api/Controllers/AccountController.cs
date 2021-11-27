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
        private readonly IJwtService _jwtService;

        public AccountController(IChinookContext chinookContext, IJwtService jwtService)
        {
            _chinookContext = chinookContext;
            _jwtService = jwtService;
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

            var crearJWTResponse = await _jwtService.CrearJWT(user);

            return Ok(crearJWTResponse);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous] // Hacemos que esta ruta sea publica
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
        {
            // Validar el jwt para obtener el user id
            var userId = _jwtService.ValidarJWT(request.Token);

            // Obtener el usuario
            var user = await _chinookContext.Users.FindAsync(userId);

            if(user == null)
            {
                return Unauthorized(new { message = "Token invalido" });
            }

            // Validar el refresh token en la BD
            var bdRefreshToken = await _chinookContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == user.UserId);

            if(bdRefreshToken == null)
            {
                return Unauthorized(new { message = "Refresh Token invalido" });
            }

            // TODO: cada refresh token deberia tener un flag de usado o implementar un tecnica de rotacion de tokens (sobreescribir los tokens para el mismo usuario)

            // Generar un nuevo token
            var crearJWTResponse = await _jwtService.CrearJWT(user);

            return Ok(crearJWTResponse);
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(User.Identity);
        }
    }
}
