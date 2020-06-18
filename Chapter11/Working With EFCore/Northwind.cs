using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Working_With_EFCore
{
    // this manages the connection to the database
    public class Northwind : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");
            optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API 
            modelBuilder.Entity<Category>()
              .Property(category => category.CategoryName)
              .IsRequired() // NOT NULL
              .HasMaxLength(15);

            // added to "fix" the decimal support in SQLite
            modelBuilder.Entity<Product>()
              .Property(product => product.Cost)
              .HasConversion<double>();

            // global filter to remove discontinued products
            modelBuilder.Entity<Product>()
              .HasQueryFilter(p => !p.Discontinued);
        }
    }
}