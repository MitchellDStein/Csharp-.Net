using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Practical.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace NorthwindWeb.Pages
{
    public class CustomersByCountryModel : PageModel
    {
        private Northwind db;

        public CustomersByCountryModel(Northwind dbData)
        {
            db = dbData;
        }

        public ILookup<string, Customer> CustomersByCountry { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Customers by Country";

            CustomersByCountry = db.Customers
                .OrderBy(c => c.Country)
                .ThenBy(c => c.CompanyName)
                .ToLookup(c => c.Country);
        }
    }
}