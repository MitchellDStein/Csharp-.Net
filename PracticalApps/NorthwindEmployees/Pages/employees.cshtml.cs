using Microsoft.AspNetCore.Mvc.RazorPages;  // PageModel
using NorthwindShared;                      // Employee
using System.Linq;                          // ToArray()
using System.Collections.Generic;           // IEnumerable<T>

namespace NorthwindEmployees.Pages
{
    public class EmployeesPageModel : PageModel
    {
        private Northwind db;
        public EmployeesPageModel(Northwind dbContent)
        {
            db = dbContent;
        }
        public IEnumerable<Employee> Employees { get; set; }
        public void OnGet()
        {
            Employees = db.Employees.ToArray();
        }
    }
}