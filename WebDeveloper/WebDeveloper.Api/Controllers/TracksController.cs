using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TracksController : ControllerBase
    {
        private readonly IChinookContext _chinookContext;

        private readonly ILogger<TracksController> _logger;

        public TracksController(IChinookContext chinookContext, ILogger<TracksController> logger)
        {
            _chinookContext = chinookContext;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetById(int id)
        {
            System.Threading.Thread.Sleep(3000);
            var track = await _chinookContext.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }
    }
}
