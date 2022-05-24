using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Whomever.Data.Entities;

namespace Whomever.Data
{
    //public class ApplicationDbContext : IdentityDbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    {
    //    }
    //}
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer();
        //}
    }
}