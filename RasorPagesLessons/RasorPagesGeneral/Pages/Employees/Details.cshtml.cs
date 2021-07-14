using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RasorPagesLessons.Models;
using RasorPagesLessons.Services;

namespace RasorPagesGeneral.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository _Repository;
        public DetailsModel(IEmployeeRepository repository)
        {
            _Repository = repository;
        }
        public Employee Employee { get; private set; }
        public IActionResult OnGet(int id)
        {
            Employee = _Repository.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound");
            return Page();
        }
    }
}
