using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.CibertecMvcIdentity.Models
{
    // Le estamos diciendo al context que use ChinookUser como objeto de User principal
    public class ChinookIdentityContext : IdentityDbContext<ChinookUser>
    {
        public ChinookIdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}
