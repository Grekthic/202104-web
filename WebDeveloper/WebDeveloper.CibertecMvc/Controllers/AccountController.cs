using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.CibertecMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IChinookContext _chinookContext;
        public AccountController(IChinookContext chinookContext)
        {
            _chinookContext = chinookContext;
        }
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedireccionarLocalmente(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            // Validar que el usuario exista en la BD
            var user = await _chinookContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            // Si llegamos hasta aqui, significa que el usuario y contrasenia son validos, entonces hay que iniciar sesion
            // 1. Crear un arreglo de claims para la identidad
            var userClaims = new List<Claim>
            {
                new Claim("DNI", user.Dni),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            // 2. Crear la identidad del usuario
            var identity = new ClaimsIdentity(userClaims, "ClaimsIdentity");

            // 3. Crear el Claims Principal para poder identificar la sesion a nivel de servidor
            var claimsPrincipal = new ClaimsPrincipal(new[] { identity });

            // 4. Iniciar sesion en el contexto de la solicitud
            await HttpContext.SignInAsync(claimsPrincipal);

            return RedireccionarLocalmente(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedireccionarLocalmente(null);
        }

        [HttpPost]
        public async Task LoginWithGoogle(string returnUrl = null)
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new GoogleChallengeProperties
            {
                // Configurar la ruta a la que se va a redireccionar luego de /signin-google
                RedirectUri = Url.Action("GoogleCallback", new { returnUrl })
            });
        }

        public IActionResult GoogleCallback(string returnUrl = null)
        {
            return RedireccionarLocalmente(returnUrl);
        }

        public IActionResult RedireccionarLocalmente(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}