using Microsoft.AspNetCore.Mvc;
using RasorPagesLessons.Models;
using RasorPagesLessons.Services;

namespace RasorPagesGeneral.ViewComponents
{
    public class HeadCountViewComponent: ViewComponent
    {
        private readonly IEmployeeRepository _repository;
        public HeadCountViewComponent(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = _repository.EmployeeCountByDept(department);
            return View(result);
        }
    }
}
