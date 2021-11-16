using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;

namespace WebDeveloper.Core.Interfaces
{
    public interface IChinookContext
    {
        DbSet<Artist> Artists { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<InvoiceLine> InvoiceLines { get; set; }
        DbSet<Track> Tracks { get; set; }
        DbSet<ChinookUser> Users { get; set; }
        DbSet<MediaType> MediaTypes { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Album> Albums { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
