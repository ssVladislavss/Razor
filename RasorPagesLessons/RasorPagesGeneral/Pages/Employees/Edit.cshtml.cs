using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RasorPagesLessons.Models;
using RasorPagesLessons.Services;

namespace RasorPagesGeneral.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _web;
        public EditModel(IEmployeeRepository repository, IWebHostEnvironment web)
        {
            _repository = repository;
            _web = web;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]// Благодаря этому атрибуту свойство доступно во всех пост запросах
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
                Employee = _repository.GetEmployee(id.Value);
            else
                Employee = new Employee();

            if (Employee == null)
                return RedirectToPage("/NotFound");
            return Page();                      
        }       
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)//Этот метод проверит есть ли картинка у нашего экземпляра, если есть, то удалит её, а потом поёдет создавать новую
                    {
                        string filePath = Path.Combine(_web.WebRootPath, "images", Employee.PhotoPath);

                        if(Employee.PhotoPath != "noimage.png")
                           System.IO.File.Delete(filePath);
                    }

                    Employee.PhotoPath = ProcessUploadedFile();
                }
                if(Employee.Id > 0)
                {
                    Employee = _repository.Update(Employee);
                    TempData["SeccessMessage"] = $"Update {Employee.Name} successfull!";
                    
                }
                else
                {
                    Employee = _repository.Add(Employee);
                    TempData["SeccessMessage"] = $"Adding {Employee.Name} successfull!";
                }


                return RedirectToPage("Employees");
            }           
                return Page();
            
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            Employee = _repository.GetEmployee(id);
            if (Notify)
                Message = "Thank you turning on notifications";
            else
                Message = "You have turned off email notifications";
        }


        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if(Photo != null) // Весь этот блок гарантирует, что новая картинка будет сохранена в папку images
            {
                string UploadsFolder = Path.Combine(_web.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(UploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
