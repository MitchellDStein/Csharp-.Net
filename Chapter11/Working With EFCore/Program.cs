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
            // QueryingCategories();
            QueryingProducts();
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

        static void QueryingProducts()
        {
            // create DB instance
            using (var db = new Northwind())
            {
                WriteLine("Products that cost more than a price, highest at top.");
                string input;
                decimal price;

                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));

                IOrderedEnumerable<Product> prods = db.Products
                    .AsEnumerable() // force client-side execution
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);

                foreach (Product item in prods)
                {
                    // WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",item.ProductID, item.ProductName, item.Cost, item.Stock);
                }

                IQueryable<Product> allProd = db.Products;

                foreach (Product item in allProd)
                {
                    WriteLine(item.SupplierID);
                }
            }
        }
    }
}
