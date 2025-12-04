using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LapWeb01.Models;

namespace LapWeb01.Data
{
    public class LapWeb01Context : DbContext
    {
        public LapWeb01Context (DbContextOptions<LapWeb01Context> options)
            : base(options)
        {
        }

        public DbSet<LapWeb01.Models.Movie> Movie { get; set; } = default!;
    }
}
