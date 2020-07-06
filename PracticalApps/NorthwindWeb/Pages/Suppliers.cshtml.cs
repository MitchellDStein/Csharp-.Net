using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; // Model-View-Controller

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

        // submitting new supplier with POST
        [BindProperty] // used to easily connect HTML elements on the web page to properties in the Supplier class
        public Supplier Supplier { get; set; }
        public IActionResult OnPost()   // responds to HTTP POST requests
        {
            if (ModelState.IsValid)     // checks property values conform to validation rules
            {
                db.Suppliers.Add(Supplier); // add new supplier to DB
                db.SaveChanges();           // save new supplier to DB
                return RedirectToPage("/suppliers");    // return to suppliers page (refreshes)
            }
            return Page();
        }
    }
}