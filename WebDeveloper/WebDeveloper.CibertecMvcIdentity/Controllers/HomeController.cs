using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace WebDeveloper.CibertecMvcIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EnviarCorreoPrueba()
        {
            await _emailService.SendAsync("mail@algo.com", "Correo de Prueba", "Hola PaperCut!");
            return View();
        }
    }
}