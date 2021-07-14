using Microsoft.EntityFrameworkCore;
using RasorPagesLessons.Models;

namespace RasorPagesLessons.Services
{
    public class Db:DbContext
    {
        public Db(DbContextOptions<Db> options): base(options) { }
        
        public DbSet<Employee> Employees { get; set; }
      
    }
}
