using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.Core.ViewModels.Reports;

namespace WebDeveloper.CibertecMvc.Models
{
    public class TrackReportsViewModel
    {
        public DateTime FechaUltimaActualizacion { get; set; }
        public IList<TopCancionVendida> ListaTopCancionesVendidas { get; set; }
        public List<ColumnSeriesData> HighChartsSerieTopCancionesVendidas { get; set; }
    }
}
