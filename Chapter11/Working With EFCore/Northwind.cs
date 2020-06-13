using Microsoft.EntityFrameworkCore;

namespace Working_With_EFCore
{
    // this manages the connection to the DB
    public class Northwind : DbContext
    {
        // these properties map to tables in the database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPAth = System.IO.Path.Combine(
            System.Environment.CurrentDirectory, "Northwind.db");
            optionsBuilder.UseSqlite($"Filename={dbPAth}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API usage to limit the length of a category name to 15
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired()   // NOT NULL
                .HasMaxLength(15);
        }
    }
}