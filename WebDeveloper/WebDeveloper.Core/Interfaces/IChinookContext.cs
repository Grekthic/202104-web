﻿using Microsoft.EntityFrameworkCore;
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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
