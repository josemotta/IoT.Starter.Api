using Microsoft.EntityFrameworkCore;
using RazorPageShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageShop.Core
{
    public class Context : DbContext
    {
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Product> Product { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
    }
}
