using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.CibertecMvc.Controllers
{
    [Authorize]
    public class ChinookUsersController : Controller
    {
        private readonly IChinookContext _chinookContext;
        public ChinookUsersController(IChinookContext chinookContext)
        {
            _chinookContext = chinookContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _chinookContext.Users.ToListAsync());
        }
    }
}