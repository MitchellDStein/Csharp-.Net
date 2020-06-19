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
            FilterAndSort();
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
    }
}
