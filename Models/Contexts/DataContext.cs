using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Models.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts):base(opts)
        {

        }

        public DbSet<Product>Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }





        
}
