using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebDeveloper.Core.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        // Agregar informacion adicional para hacer una validacion mas exhaustiva del refresh token
        // Ej.: IP, Nombre, Fecha de expiracion (del refresh token), etc.
    }
}
