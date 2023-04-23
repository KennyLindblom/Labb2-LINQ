using Labb2_LINQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ.Data
{
    internal class SkolaDBContext : DbContext
    {
        public DbSet<Elev> Elever { get; set; }
        public DbSet<Klass> Klasser { get; set; }
        public DbSet<Kurs> Kurser { get; set; }
        public DbSet<Lärare> Lärare { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = KENNYLINDBLOM;Initial Catalog=Lab2Linq;Integrated Security = True;");
        }

    }
}
