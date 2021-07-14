using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RasorPagesLessons.Models;
using RasorPagesLessons.Services;

namespace RasorPagesGeneral.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly IEmployeeRepository _db;
        public EmployeesModel(IEmployeeRepository db)
        {
            _db = db;
        }
        public IEnumerable<Employee> Employees { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Employees = _db.Search(SearchTerm);

            
        }
    }
}
