using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels.Reports;

namespace WebDeveloper.Core.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IChinookContext _context;

        public ReportsService(IChinookContext context)
        {
            _context = context;
        }

        public IList<TopCancionVendida> ObtenerTopDeCancionesVendidas(int n)
        {
            //var query = _context.InvoiceLines
            //    .Include(il=>il.Track)
            //    .GroupBy(il => il.TrackId) // key, data[] -> 1, InvoiceLine Objects
            //    .Select(g => new TopCancionVendida
            //    {
            //        // Sabemos que los datos de la cancion son el mismo para todo el grupo
            //        TrackId = g.Key,
            //        Quantity = g.Sum(g1 => g1.Quantity),
            //    })
            //    .OrderByDescending(t => t.Quantity)
            //    .Take(n);

            var query = _context.Tracks
                .Select(t => new TopCancionVendida
                {
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    Quantity = t.InvoiceLines.Sum(il => il.Quantity),
                    AlbumTitle = t.Album.Title,
                    ArtistName = t.Album.Artist.Name,
                })
                .OrderByDescending(tcv => tcv.Quantity)
                .Take(n);

            return query.ToList();
        }
    }
}
