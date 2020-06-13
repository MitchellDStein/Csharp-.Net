using System;
using static System.Console;
using Working_With_EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Working_With_EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryingCategories();
        }
        
        // define QueryingCategories method to
        static void QueryingCategories()
        {
            // create an instance of Northwind class to manage DB
            using (var db = new Northwind())
            {
                WriteLine("Categories and how many products they have: ");

                // a query to get all catagories and their related products
                IQueryable<Category> cats = db.Categories.Include(c => c.Products);
                
                // enum through the categories, outputting the name and number of products to each one.
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
    }
}
