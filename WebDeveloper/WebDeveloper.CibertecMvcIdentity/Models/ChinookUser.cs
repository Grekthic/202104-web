using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.CibertecMvcIdentity.Models
{
    // Este User personalizado tiene que heredar de IdentityUser que es del framework para funcionar con el modelo de ASP.Net Identity
    public class ChinookUser : IdentityUser
    {
        // Agregar campos personalizados
        [Required]
        [MaxLength(8)]
        public string Dni { get; set; }
    }
}
