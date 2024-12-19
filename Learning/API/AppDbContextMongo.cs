using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class AppDbContextMongo(DbContextOptions<AppDbContextMongo> options) : DbContext(options)
    {
		public DbSet<Product> Products { get; set; }
    }
}