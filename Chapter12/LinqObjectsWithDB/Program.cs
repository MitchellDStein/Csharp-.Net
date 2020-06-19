using System;
using static System.Console;
using LinqObjectsWithDB;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace LinqObjectsWithDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // FilterAndSort();
            JoinCatsAndProds();
        }

        static void FilterAndSort()
        {
            using (var db = new Northwind())
            {
                var query = db.Products
                    .AsEnumerable() // fix error for client evaluation
                    .Where(product => product.UnitPrice < 10M)          // IQueryable<Product>
                    .OrderByDescending(product => product.UnitPrice)    // IOrderedQueryable<Product>
                    .Select(product => new
                    {// anonymous type
                        product.ProductID,
                        product.ProductName,
                        product.UnitPrice
                    });

                WriteLine("Products that cost less than $10:");
                foreach (var item in query)
                {
                    WriteLine("{0}: {1,-35} {2:C}",
                        item.ProductID, item.ProductName, item.UnitPrice);
                }
                WriteLine();
            }
        }

        static void JoinCatsAndProds()
        {
            using (var db = new Northwind())
            {
                // join every ptoduct to its cat to return 77 matches
                var joinQuery = db.Categories.Join(
                    inner: db.Products,
                    outerKeySelector: category => category.CategoryID,
                    innerKeySelector: product => product.CategoryID,
                    resultSelector: (c, p) => new { c.CategoryName, p.ProductName, p.ProductID })
                    .OrderBy(cp => cp.ProductID);

                foreach (var item in joinQuery)
                {
                    WriteLine("{0}: {1} is in {2}",
                        item.ProductID, item.ProductName, item.CategoryName);
                }
            }
        }

        static void GroupJoinCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                // group all products by their category to return 8 matches
                var queryGroup = db.Categories
                    .AsEnumerable()
                        .GroupJoin(
                        inner: db.Products,
                        outerKeySelector: category => category.CategoryID,
                        innerKeySelector: product => product.CategoryID,
                        resultSelector: (c, matchingProducts) => new
                        {
                            c.CategoryName,
                            Products = matchingProducts.OrderBy(p => p.ProductName)
                        });

                foreach (var item in queryGroup)
                {
                    WriteLine("{0} has {1} products.",
                        item.CategoryName,
                        item.Products.Count());
                    foreach (var product in item.Products)
                    {
                        WriteLine($" {product.ProductName}");
                    }
                }
            }
        }
    }
}
