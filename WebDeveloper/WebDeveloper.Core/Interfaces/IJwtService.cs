using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Responses.Jwt;

namespace WebDeveloper.Core.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// 1. A partir de un usuario, crea un JWT con los claims del usuario
        /// 2. Asimismo, crear el refresh token para este usuario
        /// </summary>
        /// <param name="user">El ChinookUser para el que se creara el token</param>
        /// <returns>El token, su fecha de expiracion y el refresh token</returns>
        Task<CrearJWTResponse> CrearJWT(ChinookUser user);

        /// <summary>
        /// Crear un refresh token mediante un algoritmo criptologico
        /// Guardar el refresh el token en la BD
        /// </summary>
        /// <param name="user">El ChinookUser para el que se creara el token</param>
        /// <returns>El registro refresh token creado</returns>
        Task<RefreshToken> CrearRefreshToken(ChinookUser user);

        /// <summary>
        /// Valida que el JWT haya sido generado por nuestra app
        /// </summary>
        /// <param name="jwt">el token a validar</param>
        /// <returns>Devuelve el user id obtenido del token</returns>
        int ValidarJWT(string jwt);
    }
}
