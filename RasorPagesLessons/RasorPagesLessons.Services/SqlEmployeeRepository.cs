using Microsoft.EntityFrameworkCore;
using RasorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RasorPagesLessons.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly Db _db;
        public SqlEmployeeRepository(Db db)
        {
            _db = db;
        }
        public Employee Add(Employee NewEmployee)
        {
            //_db.Employees.Add(NewEmployee);
            //_db.SaveChanges();
            //return NewEmployee;

            _db.Database.ExecuteSqlRaw("spAddNewEmployee {0}, {1}, {2}, {3}", 
                                        NewEmployee.Name, NewEmployee.Email, NewEmployee.PhotoPath, NewEmployee.Department);
            return NewEmployee;
        }

        public Employee Delete(int id)
        {
            var deleteResult = _db.Employees.Find(id);
            if(deleteResult != null)
            {
                _db.Employees.Remove(deleteResult);
                _db.SaveChanges();
            }
            return deleteResult;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _db.Employees;
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
            //return _db.Employees;
            return _db.Employees
                      .FromSqlRaw<Employee>("SELECT * From Employees")
                      .ToList();
        }

        public Employee GetEmployee(int id)
        {
            //return _db.Employees.Find(id);
            return _db.Employees //это запрос к базе данных, он выполняет поиск по id через созданную процедуру в самой базе данных, это удобно тем, что если проект высоконагруженный, то перезапускать сервер не нужно
                      .FromSqlRaw<Employee>("CodeFirstSpGetEmployeeById {0}", id)
                      .ToList()
                      .FirstOrDefault();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _db.Employees;

            return _db.Employees.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employeeUpdate = _db.Employees.Attach(updatedEmployee);//этот метод обновляет старую запись в базе на новую
            employeeUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return updatedEmployee;
        }
    }
}
