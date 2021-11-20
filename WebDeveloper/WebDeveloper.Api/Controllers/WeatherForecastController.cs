using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebDeveloper.Api.Controllers
{
    public class GenericParam
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
    }
    // Ruta /WeatherForecast
    [ApiController]
    [Route("[controller]")]
    [Route("otra-ruta")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET /WeatherForecast/
        [HttpGet]
        [HttpGet("otro-get")]
        [HttpGet("/otro-get-root")] // No recomendable
        public IEnumerable<WeatherForecast> ObtenerTodo()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET /WeatherForecast/saludar/nombre?apellido=apellido
        [HttpGet("saludar/{nombre}")]
        public string ObtenerString(string nombre, string apellido)
        {
            return $"Hola {nombre} {apellido}";
        }

        // GET /WeatherForecast/saludar/nombre/apellido
        [HttpGet("saludar/{nombre}/{apellido}")]
        public string ObtenerString2(string nombre, string apellido)
        {
            return $"Hola 2 {nombre} {apellido}";
        }

        // GET /WeatherForecast/saludar/nombre?p1=p1&p2=p2
        [HttpGet("saludar-3/{nombre}")]
        public string ObtenerString3(string nombre, [FromQuery]GenericParam param)
        {
            return $"Hola 3 {nombre} {param.P1} {param.P2}";
        }
    }
}
