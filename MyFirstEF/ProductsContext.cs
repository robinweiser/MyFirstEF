using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static MyFirstEF.ProbNames;

namespace MyFirstEF
{
    public class ProbNames
    {
        public const string Title = "_title";
    }
    class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Produkte");
            modelBuilder.Entity<Product>().ToTable("Produkte");
            //modelBuilder.Entity<Product>().Property(m => m.name).HasField(Title);
            modelBuilder.Entity<Product>().Property(m => m.Description).HasMaxLength(60).IsRequired(false);
            modelBuilder.Entity<Product>().HasIndex(m => m.Name);
            modelBuilder.Entity<Supplier>().OwnsOne(a => a.Address).ToTable("Addresses").OwnsOne(a => a.Country).ToTable("Countries");
            //modelBuilder.Entity<Product>().HasOne("Productgroup");
           

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
