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
            var query = _context.InvoiceLines
                .GroupBy(il => il.TrackId)
                .Select(g => new TopCancionVendida
                {
                    TrackId = g.Key,
                    Quantity = g.Sum(g1 => g1.Quantity)
                })
                .OrderByDescending(t=>t.Quantity)
                .Take(n);
            return query.ToList();
        }
    }
}
