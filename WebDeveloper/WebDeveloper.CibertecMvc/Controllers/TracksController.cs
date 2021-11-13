using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDeveloper.CibertecMvc.Models;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.CibertecMvc.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IChinookContext _chinookContext;
        public TracksController(ITrackService trackService, IChinookContext chinookContext)
        {
            _trackService = trackService;
            _chinookContext = chinookContext;
        }
        public async Task<IActionResult> Index()
        {
            // Obtener toda la data
            var data = await _trackService.ObtenerListaTracks();
            return View(data);
        }

        public async Task<IActionResult> FormPartial()
        {
            var lista = await _chinookContext.MediaTypes
                .Select(mt => new SelectListItem
                {
                    Text = mt.Name,
                    Value = mt.MediaTypeId.ToString()
                })
                .ToListAsync();
            //ViewBag.MediaTypeList = lista;
            // Crear el modelo de la vista parcial
            var model = new TrackModalFormViewModel
            {
                ModalTitle = "Crear una cancion",
                Track = new Track(),
                MediaTypeList = lista
            };
            return PartialView("_TrackModalFormPartial", model);
        }
    }
}