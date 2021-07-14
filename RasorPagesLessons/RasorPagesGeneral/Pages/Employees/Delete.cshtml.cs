using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RasorPagesLessons.Models;
using RasorPagesLessons.Services;

namespace RasorPagesGeneral.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _web;
        public DeleteModel(IEmployeeRepository repository, IWebHostEnvironment web)
        {
            _repository = repository;
            _web = web;
        }


        [BindProperty]
        public Employee Employee { get; set; }

        public IActionResult OnGet(int id)
        {
            Employee = _repository.GetEmployee(id);

            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            Employee deletedEmployee = _repository.Delete(Employee.Id);
            if (deletedEmployee.PhotoPath != null)//Ётот метод проверит есть ли картинка у нашего экземпл€ра, если есть, то удалит еЄ, а потом поЄдет создавать новую
            {
                string filePath = Path.Combine(_web.WebRootPath, "images", deletedEmployee.PhotoPath);

                if (deletedEmployee.PhotoPath != "noimage.png")
                    System.IO.File.Delete(filePath);
            }
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound");
            return RedirectToPage("Employees");
        }
    }
}
