using Microsoft.EntityFrameworkCore;
using System;
using XyzSystem.Models.Domain;

namespace XyzSystem.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>()
                .HasKey(m => new { m.ProductCode, m.CategoryId });
        }
    }
}
