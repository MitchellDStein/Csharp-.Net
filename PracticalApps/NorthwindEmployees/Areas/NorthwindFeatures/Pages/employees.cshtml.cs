using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Practical.Shared;                     // Employee
using System.Linq;                         // ToArray()
using System.Collections.Generic;          // IEnumerable<T>

namespace NorthwindEmployees.Pages
{
  public class EmployeesPageModel : PageModel
  {
    private Northwind db;

    public EmployeesPageModel(Northwind injectedContext)
    {
      db = injectedContext;
    }

    public IEnumerable<Employee> Employees { get; set; }

    public void OnGet()
    {
      Employees = db.Employees.ToArray();
    }
  }
}