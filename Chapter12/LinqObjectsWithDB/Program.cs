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
            // JoinCatsAndProds();
            // GroupjoinCategoriesAndProducts();
            // AggregateProducts();
            // CustomExtensionMethods();
            // OutputProductsAsXml();
            ReadXML();
        }

        static void FilterAndSort()
        {
            using (var db = new Northwind())
            {
                var query = db.Products
                    .AsEnumerable() // fix error for client evaluation
                                    // .ProcessSequence()
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

        static void GroupjoinCategoriesAndProducts()
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
                        WriteLine($"    {product.ProductName}");
                    }
                }
            }
        }

        static void AggregateProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("{0,-25} {1,10}",
                  "Product count:",
                  db.Products.AsEnumerable().Count());

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                  "Highest product price:",
                  db.Products.AsEnumerable().Max(p => p.UnitPrice));

                WriteLine("{0,-25} {1,10:N0}",
                  "Sum of units in stock:",
                  db.Products.AsEnumerable().Sum(p => p.UnitsInStock));

                WriteLine("{0,-25} {1,10:N0}",
                  "Sum of units on order:",
                  db.Products.AsEnumerable().Sum(p => p.UnitsOnOrder));

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                  "Average unit price:",
                  db.Products.AsEnumerable().Average(p => p.UnitPrice));

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                  "Value of units in stock:",
                  db.Products.AsEnumerable()
                    .Sum(p => p.UnitPrice * p.UnitsInStock));
            }
        }

        static void CustomExtensionMethods()
        {
            using (var db = new Northwind())
            {
                WriteLine("Mean units in stock: {0:N0}",
                db.Products.Average(p => p.UnitsInStock));

                WriteLine("Mean unit price: {0:$#,##0.00}",
                db.Products.AsEnumerable().Average(p => p.UnitPrice));

                WriteLine("Median units in stock: {0:N0}",
                db.Products.Median(p => p.UnitsInStock));

                WriteLine("Median unit price: {0:$#,##0.00}",
                db.Products.Median(p => p.UnitPrice));

                WriteLine("Mode units in stock: {0:N0}",
                db.Products.Mode(p => p.UnitsInStock));

                WriteLine("Mode unit price: {0:$#,##0.00}",
                db.Products.Mode(p => p.UnitPrice));
            }
        }

        static void OutputProductsAsXml()
        {
            using (var db = new Northwind())
            {
                var productsForXml = db.Products.ToArray();

                var xml = new XElement("products",
                    from p in productsForXml
                    select new XElement("product",
                            new XAttribute("id", p.ProductID),
                            new XAttribute("price", p.UnitPrice),
                        new XElement("name", p.ProductName)));

                WriteLine(xml.ToString());
            }
        }

        static void ReadXML()
        {
            XDocument doc = XDocument.Load("settings.xml");

            var xmlFile = doc.Descendants("appSettings")
                .Descendants("add")
                .Select(node => new
                {
                    Key = node.Attribute("key").Value,
                    Value = node.Attribute("value").Value
                })
                .ToArray();

            foreach (var item in xmlFile)
            {
                WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}