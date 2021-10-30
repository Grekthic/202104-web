using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.Entities;

namespace WebDeveloper.Core.Interfaces
{
    public interface IChinookContext
    {
        DbSet<Artist> Artists { get; set; }
    }
}
