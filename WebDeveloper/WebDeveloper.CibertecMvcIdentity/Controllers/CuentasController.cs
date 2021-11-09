using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using WebDeveloper.CibertecMvcIdentity.Models;

namespace WebDeveloper.CibertecMvcIdentity.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<ChinookUser> _userManager;
        private readonly SignInManager<ChinookUser> _signInManager;
        private readonly IEmailService _emailService;

        public CuentasController(UserManager<ChinookUser> userManager, SignInManager<ChinookUser> signInManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        public IActionResult IniciaSesion()
        {
            return View();
        }

        public async Task<IActionResult> CrearUsuarioPrueba()
        {
            // Crear un usuario de prueba
            var result = await _userManager.CreateAsync(new ChinookUser { UserName = "mail@algo.com", Email = "mail@algo.com", Dni = "12345678" }, "1234");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciaSesion(string email, string password)
        {
            // Tratar de obtener el usuario de BD
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Iniciar Sesion (verificando la contrasenia)
                var resultadoInicioSesion = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (resultadoInicioSesion.Succeeded)
                {
                    // Si todo esta bien, redirecionar a Home/Index
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string email, string password)
        {
            // Crear el usuario
            var user = new ChinookUser
            {
                UserName = email,
                Email = email,
                Dni = "11111111"
            };

            // Registrar el usuario
            var resultadoRegistro = await _userManager.CreateAsync(user, password);

            // Si la operacion fue satisfactoria, enviar el correo de verificacion
            if (resultadoRegistro.Succeeded)
            {
                // Generar el token para que el usuario confirme su cuenta
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Construir un link para que el usuario al hacer clic, lo redireccione a verificar la cuenta
                // https://dominio/Cuentas/VerificarCuenta?userId=valor&token=otro-valor
                var linkUrl = Url.Action("VerificarCuenta", "Cuentas", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

                // Enviamos el correo
                await _emailService.SendAsync(user.Email, "Confirma tu cuenta", $"<a href=\"{linkUrl}\">Verificar Email</a>", isHtml: true);

                // TODO: redireccionar a una vista de confirmacion
            }
            return View();
        }
    }
}