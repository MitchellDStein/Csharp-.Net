using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using NorthwindShared;

namespace NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        private Northwind db; // for database access

        public IEnumerable<string> Suppliers { get; set; }

        public SuppliersModel(Northwind dbData)
        {
            db = dbData;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Suppliers";
            // Suppliers = new[] { "Alpha Co", "Beta Limited", "Gamma Corp" };
            Suppliers = db.Suppliers.Select(s => s.CompanyName);
        }
    }
}