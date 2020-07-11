using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDepartments.Models;

namespace WebApiDepartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        DepartmentsContext db;
        
        public DepartmentsController(DepartmentsContext context)
        {
            db = context;
            
            if (!db.Departments.Any())
            {
                db.Departments.Add(new Department { Name = "Technical" });
                db.Departments.Add(new Department { Name = "Marketing" });
                db.Departments.Add(new Department { Name = "IT" });
                db.Departments.Add(new Department { Name = "Bookkering" });
                db.Departments.Add(new Department { Name = "Legal" });
                db.Departments.Add(new Department { Name = "Maintenance" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            return await db.Departments.ToListAsync();
        }

        // GET api/departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            Department department = await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (department == null)
                return NotFound();
            return new ObjectResult(department);
        }

        // POST api/departments
        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            db.Departments.Add(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }

        // PUT api/departments/
        [HttpPut]
        public async Task<ActionResult<Department>> Put(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            if (!db.Departments.Any(x => x.Id == department.Id))
            {
                return NotFound();
            }

            db.Update(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }

        // DELETE api/departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Delete(int id)
        {
            Department department = db.Departments.FirstOrDefault(x => x.Id == id);
            
            if (department == null)
            {
                return NotFound();
            }
            
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
            return Ok(department);
        }
    }
}
