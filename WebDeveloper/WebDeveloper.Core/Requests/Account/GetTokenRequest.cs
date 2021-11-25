using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebDeveloper.Core.Requests.Account
{
    public class GetTokenRequest
    {
        [Required(ErrorMessage = "El password es requerido")]
        public string Password { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email invalido")]
        [Required]
        public string Email { get; set; }
    }
}
