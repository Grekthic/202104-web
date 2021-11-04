using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.ViewModels.Reports;

namespace WebDeveloper.Core.Interfaces
{
    public interface IReportsService
    {
        IList<TopCancionVendida> ObtenerTopDeCancionesVendidas(int n);
        //void ObtenerCantidadDeAlbumesPorArtista();
    }
}
