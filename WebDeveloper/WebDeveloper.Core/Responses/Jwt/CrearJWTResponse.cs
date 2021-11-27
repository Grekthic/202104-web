using System;
using System.Collections.Generic;
using System.Text;

namespace WebDeveloper.Core.Responses.Jwt
{
    public class CrearJWTResponse
    {
        /// <summary>
        /// El JWT apra realizar las consultas
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// La fecha en Unix Time en la que expira el JWT
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// El valor del Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
