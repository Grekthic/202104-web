using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.Infra
{
    public class ChinookContext : DbContext, IChinookContext
    {
        public ChinookContext()
        {
        }

        public ChinookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
    }
}
