
using DefontanaTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefontanaTest.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Local> Local { get; set; }
        public DbSet<Brand> Marca { get; set; }
        public DbSet<Product> Producto { get; set; }
        public DbSet<Sale> Venta { get; set; }
        public DbSet<SaleDetail> VentaDetalle { get; set; }
    }
}
