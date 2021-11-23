using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IChinookContext _chinookContext;

        private readonly ILogger<ArtistsController> _logger;

        public ArtistsController(IChinookContext chinookContext, ILogger<ArtistsController> logger)
        {
            _chinookContext = chinookContext;
            _logger = logger;
        }

        /// <summary>
        /// Esto es un comentario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> Get()
        {
            _logger.LogInformation("Se realizara la consulta del Artistas");
            var resultados = await _chinookContext.Artists.ToListAsync();
            _logger.LogInformation($"Se obtuvo la siguiente cantidad de Artistas {resultados.Count}");
            return Ok(resultados);
        }

        // POST /artists -> Crear un artista
        // 3.1 -> System.Text.Json
        // Json.Net
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Artist artist)
        {
            _logger.LogInformation("Body recibido {@artist}", artist);
            // Guardar el registro
            await _chinookContext.Artists.AddAsync(artist);
            await _chinookContext.SaveChangesAsync();
            return Ok(artist);
        }
    }
}
