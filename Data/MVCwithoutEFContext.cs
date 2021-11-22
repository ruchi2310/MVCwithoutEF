using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCwithoutEF.Models;

namespace MVCwithoutEF.Data
{
    public class MVCwithoutEFContext : DbContext
    {
        public MVCwithoutEFContext (DbContextOptions<MVCwithoutEFContext> options)
            : base(options)
        {
        }

        public DbSet<BookViewModel> BookViewModel { get; set; }
        public DbSet<BookAddorEdit> BookAddorEdit { get; set; }
    }
}
