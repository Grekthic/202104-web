using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Responses.Jwt;

namespace WebDeveloper.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IChinookContext chinookContext;
        private readonly TokenValidationParameters tokenValidationParameters;

        public JwtService(IChinookContext chinookContext, TokenValidationParameters tokenValidationParameters)
        {
            this.chinookContext = chinookContext;
            this.tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<CrearJWTResponse> CrearJWT(ChinookUser user)
        {
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

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenParams);

            // Crear el refresh token
            var refreshToken = await CrearRefreshToken(user);

            return new CrearJWTResponse
            {
                Token = jwt,
                RefreshToken = refreshToken.Token,
                Expires = new DateTimeOffset(expirationDate).ToUnixTimeSeconds()
            };
        }

        public async Task<RefreshToken> CrearRefreshToken(ChinookUser user)
        {
            // Generar un refresh token aleatorio
            string token;
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                cryptoServiceProvider.GetBytes(randomBytes);
                token = Convert.ToBase64String(randomBytes);
            }

            // Guardar el Refresh token en la BD
            var newRefreshToken = new RefreshToken
            {
                CreatedAt = DateTime.Now,
                Token = token,
                UserId = user.UserId
            };

            await chinookContext.RefreshTokens.AddAsync(newRefreshToken);

            // Guardar los cambios
            await chinookContext.SaveChangesAsync();

            return newRefreshToken;
        }

        public int ValidarJWT(string jwt)
        {
            if(string.IsNullOrEmpty(jwt))
            {
                return 0;
            }

            // Crear un token handler para hacer la validacion
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(jwt, tokenValidationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userIdString = jwtToken.Claims.First(c => c.Type == "sub").Value;

                return int.TryParse(userIdString, out int userId) ? userId : 0;
            }   
            catch (Exception ex)
            {
                // Lo optimo seria escribir la excpecion en un log
                return 0;
            }
        }
    }
}
