using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDepartments.Models
{
    public class DepartmentsContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
       
        public DepartmentsContext(DbContextOptions<DepartmentsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
