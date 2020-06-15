using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Working_With_EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Show SQL queries?  (Y/N): ");
            bool showQueries = (ReadKey().Key == ConsoleKey.Y);
            WriteLine();
            // QueryingCategories(showQueries);
            // QueryingProducts(showQueries);
            // QueryingWithLike(showQueries);
            if (AddProduct(6, "Bob's Burgers", 500M))
            {
                WriteLine("Add product successful.");
            }
            ListProducts();
        }

        // define QueryingCategories method to
        static void QueryingCategories(bool showQueries)
        {
            // create an instance of Northwind class to manage DB
            using (var db = new Northwind())
            {
                if (showQueries)
                {
                    var customLogger = db.GetService<ILoggerFactory>();
                    customLogger.AddProvider(new ConsoleLoggerProvider());
                }

                // a query to get all categories and their related products
                IQueryable<Category> cats;
                //  = db.Categories;                // commented out for explicit loading
                //  .Include(c => c.Products);      // commented out for lazy loading

                db.ChangeTracker.LazyLoadingEnabled = false;
                Write("Enable eager loading? (Y/N): ");
                bool eagerloading = (ReadKey().Key == ConsoleKey.Y);
                bool explicitloading = false;
                WriteLine();

                if (eagerloading)
                {
                    cats = db.Categories.Include(c => c.Products);
                }
                else
                {
                    cats = db.Categories;
                    Write("Enable explicit loading? (Y/N): ");
                    explicitloading = (ReadKey().Key == ConsoleKey.Y);
                    WriteLine();
                }

                WriteLine("Categories and how many products they have: ");
                // enum through the categories, outputting the name and number of products to each one.
                foreach (Category c in cats)
                {
                    if (explicitloading)
                    {
                        Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                        ConsoleKeyInfo key = ReadKey();
                        WriteLine();

                        if (key.Key == ConsoleKey.Y)
                        {
                            var products = db.Entry(c).Collection(c2 => c2.Products);
                            if (!products.IsLoaded) products.Load();
                        }
                    }
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }

        static void QueryingProducts(bool showQueries)
        {
            // create DB instance
            using (var db = new Northwind())
            {
                if (showQueries)
                {
                    var customLogger = db.GetService<ILoggerFactory>();
                    customLogger.AddProvider(new ConsoleLoggerProvider());
                }

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
                    WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.", item.ProductID, item.ProductName, item.Cost, item.Stock);
                }
            }
        }

        static void QueryingWithLike(bool showQueries)
        {
            using (var db = new Northwind())
            {
                if (showQueries)
                {
                    var customLogger = db.GetService<ILoggerFactory>();
                    customLogger.AddProvider(new ConsoleLoggerProvider());
                }
                Write("Enter a product name to search: ");
                string input = ReadLine();

                IQueryable<Product> products = db.Products
                    .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));


                if (products.Count() < 1)
                {
                    WriteLine("No items similar to search");
                }
                else
                {
                    foreach (Product item in products)
                    {
                        WriteLine("{0} has {1} units in stock.\nDiscontinued? {2}",
                                    item.ProductName, item.Stock, item.Discontinued ? "Yes" : "No");
                    }
                }
            }
        }

        static bool AddProduct(int categoryID, string productName, decimal? price)
        {
            using (var db = new Northwind())
            {
                var newProduct = new Product
                {
                    CategoryID = categoryID,
                    ProductName = productName,
                    Cost = price
                };

                // mark product as added in change tracking
                db.Products.Add(newProduct);

                // save tracked changes to database 
                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        static void ListProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}",
                "ID", "Product Name", "Cost", "Stock", "Disc.");

                foreach (var item in db.Products.OrderByDescending(p => p.Cost))
                {
                    WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                    item.ProductID, item.ProductName, item.Cost,
                    item.Stock, item.Discontinued);
                }
            }
        }
    }
}
