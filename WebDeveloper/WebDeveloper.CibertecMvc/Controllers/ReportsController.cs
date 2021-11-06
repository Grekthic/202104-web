using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.CibertecMvc.Models;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels.Reports;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.CibertecMvc.Controllers
{
    // Controladores (clases) -> Acciones (metodos)
    // URL -> /reports/index ~ /reports
    // Configurar en el enrutador
    public class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }
        public IActionResult Index()
        {
            // Para enviar data del controlador hacia la vista
            ViewData["nombre"] = "Arturo";
            ViewBag.Nombre2 = "Arturo 2";
            ViewBag.Arreglo = new string[] { "uno", "dos" };
            return View();
        }

        public IActionResult Tracks()
        {
            var top3 = _reportsService.ObtenerTopDeCancionesVendidas(3);

            var viewModel = new TrackReportsViewModel()
            {
                FechaUltimaActualizacion = DateTime.Now,
                ListaTopCancionesVendidas = top3,
                HighChartsSerieTopCancionesVendidas = top3.Select(t3 => new ColumnSeriesData
                {
                    Y = t3.Quantity
                }).ToList()
            };
            return View(viewModel);
        }
    }
}
