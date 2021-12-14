using Microsoft.EntityFrameworkCore;
using TranThanhDat_Exercises2.Data.Configuration;
using TranThanhDat_Exercises2.Data.Entities;
namespace TranThanhDat_Exercises2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
