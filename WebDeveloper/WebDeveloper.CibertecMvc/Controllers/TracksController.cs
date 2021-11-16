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

        /// <summary>
        /// Este metodo se invoca al momento de abrir el modal para generar el contenido del formulario
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FormPartial(int trackId = 0)
        {
            // Crear el objeto track que se va a devolver
            var track = new Track();
            if (trackId != 0)
            {
                // Significa que queremos editar el track
                // Obtener la informacion del track de la BD
                track = await _chinookContext.Tracks.FindAsync(trackId);
            }

            // Validar si el track existe
            if (track == null)
            {
                return new NotFoundObjectResult(new { trackId });
            }
            // Obtener la list a de albumes
            var listaAlbumes = await _chinookContext.Albums.Include(a => a.Artist).ToListAsync();
            // Crear el modelo de la vista parcial
            var model = new TrackModalFormViewModel
            {
                ModalTitle = $"{(track.TrackId == 0 ? "Crear" : "Editar")} una cancion",
                Track = track,
                MediaTypeList = await _chinookContext.MediaTypes
                    .OrderBy(mt => mt.Name)
                    .Select(mt => new SelectListItem
                    {
                        Text = mt.Name,
                        Value = mt.MediaTypeId.ToString()
                    })
                    .ToListAsync(),
                GenreList = await _chinookContext.Genres
                    .OrderBy(g => g.Name)
                    .Select(g => new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.GenreId.ToString()
                    })
                    .ToListAsync(),
                AlbumList = listaAlbumes
                    .Select(a => new SelectListItem
                    {
                        Text = $"{a.Artist.Name} - {a.Title}", // Artista - Album
                        Value = a.AlbumId.ToString()
                    })
                    .OrderBy(sli => sli.Text)

            };
            return PartialView("_TrackModalFormPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAjax(Track newTrack)
        {
            // Validar el modelo
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Modelo invalido" });
            }

            // Si el track id es 0, significa que es una insercion
            if (newTrack.TrackId == 0)
            {
                // Insertar el nuevo registro
                await _chinookContext.Tracks.AddAsync(newTrack);
            }
            else
            {
                // Actualizar
                // 1 - Obtener el registro de la BD
                var currentTrack = await _chinookContext.Tracks.FindAsync(newTrack.TrackId);
                if (currentTrack == null)
                {
                    return new JsonResult(new { success = false, message = "Track not Found" });
                }

                // Setear los nuevos valores
                currentTrack.Name = newTrack.Name;
                currentTrack.GenreId = newTrack.GenreId;
                currentTrack.AlbumId = newTrack.AlbumId;
                currentTrack.MediaTypeId = newTrack.MediaTypeId;
                currentTrack.UnitPrice = newTrack.UnitPrice;
            }

            // Commit de la operacion
            var saveResult = await _chinookContext.SaveChangesAsync();

            if(saveResult <= 0)
            {
                return new JsonResult(new { success = false, message = "Ningun Registro fue Afectado" });
            }

            // Devolver la respuesta satisfactoria
            return new JsonResult(new { success = true });
        }
    }
}