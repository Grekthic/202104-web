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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Se realizara la consulta del Artistas");
            var resultados = await _chinookContext.Artists.ToListAsync();
            _logger.LogInformation($"Se obtuvo la siguiente cantidad de Artistas {resultados.Count}");
            return Ok(resultados);
        }
    }
}
