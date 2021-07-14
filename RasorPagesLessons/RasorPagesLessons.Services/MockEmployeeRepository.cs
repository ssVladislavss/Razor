using RasorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasorPagesLessons.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _EmployeeList;
        public MockEmployeeRepository()
        {
            _EmployeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id=0,
                    Name="Mary",
                    Email="Mary@example.com",
                    PhotoPath="avatar2.png",
                    Department=Dept.HR
                },
                new Employee()
                {
                    Id=1,
                    Name="Mark",
                    Email="Mark@example.com",
                    PhotoPath="avatar.png",
                    Department=Dept.IT
                },
                new Employee()
                {
                    Id=2,
                    Name="Vlad",
                    Email="Vlad@example.com",
                    PhotoPath="avatar1.png",
                    Department=Dept.HR
                },
                new Employee()
                {
                    Id=3,
                    Name="Tanya",
                    Email="Tanya@example.com",
                    PhotoPath="avatar3.png",
                    Department=Dept.Payroll
                },
                new Employee()
                {
                    Id=4,
                    Name="Denis",
                    Email="Denis@example.com",
                    PhotoPath="avatar4.png",
                    Department=Dept.None
                },
                new Employee()
                {
                    Id=5,
                    Name="Valera",
                    Email="Valera@example.com",                    
                    Department=Dept.IT
                },
            };
        }

        public Employee Add(Employee NewEmployee)
        {
            NewEmployee.Id = _EmployeeList.Max(x => x.Id) + 1;
            _EmployeeList.Add(NewEmployee);
            return NewEmployee;
        }
        public Employee Delete(int id)
        {
            var delete = _EmployeeList.FirstOrDefault(x => x.Id == id);
            if(delete != null)
                _EmployeeList.Remove(delete);
            return delete;
        }
        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _EmployeeList;
            if (dept.HasValue)
                query = query.Where(x => x.Department == dept.Value);

            return query.GroupBy(x => x.Department)
                                .Select(x => new DeptHeadCount()
                                {
                                    Department = x.Key.Value,
                                    Count = x.Count()
                                }).ToList();
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _EmployeeList;
        }
        public Employee GetEmployee(int id)
        {
            var employee = _EmployeeList.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _EmployeeList;

            return _EmployeeList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public Employee Update(Employee updatedEmployee)
        {
            Employee employee = _EmployeeList.FirstOrDefault(x => x.Id == updatedEmployee.Id);
            if(employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Department = updatedEmployee.Department;
                employee.PhotoPath = updatedEmployee.PhotoPath;
            }
            return employee;
        }
    }
}
