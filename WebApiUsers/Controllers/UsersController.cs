using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsers.Models;

namespace WebApiUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UsersContext db;
        DepartmentsContext dbDeps;

        public UsersController(UsersContext context, DepartmentsContext contextDeps)
        {
            db = context;
            dbDeps = contextDeps;

            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Andrew Thomas", Department = dbDeps.Departments.Where(d => d.Name == "Technical").FirstOrDefault() });
                db.Users.Add(new User { Name = "Bill Moore", Department = dbDeps.Departments.Where(d => d.Name == "Technical").FirstOrDefault() });
                db.Users.Add(new User { Name = "Keiran Moyer", Department = dbDeps.Departments.Where(d => d.Name == "Marketing").FirstOrDefault() });
                db.Users.Add(new User { Name = "Isobella Walmsley", Department = dbDeps.Departments.Where(d => d.Name == "Marketing").FirstOrDefault() });
                db.Users.Add(new User { Name = "Miruna Bullock", Department = dbDeps.Departments.Where(d => d.Name == "IT").FirstOrDefault() });
                db.Users.Add(new User { Name = "Kier Almond", Department = dbDeps.Departments.Where(d => d.Name == "IT").FirstOrDefault() });
                db.Users.Add(new User { Name = "Huzaifa Petersen", Department = dbDeps.Departments.Where(d => d.Name == "Bookkering").FirstOrDefault() });
                db.Users.Add(new User { Name = "Akash Roberts", Department = dbDeps.Departments.Where(d => d.Name == "Bookkering").FirstOrDefault() });
                db.Users.Add(new User { Name = "Gino Zhang", Department = dbDeps.Departments.Where(d => d.Name == "Legal").FirstOrDefault() });
                db.Users.Add(new User { Name = "Kya Mcpherson", Department = dbDeps.Departments.Where(d => d.Name == "Legal").FirstOrDefault() });
                db.Users.Add(new User { Name = "Olli Conroy", Department = dbDeps.Departments.Where(d => d.Name == "Maintenance").FirstOrDefault() });
                db.Users.Add(new User { Name = "Fynn Orozco", Department = dbDeps.Departments.Where(d => d.Name == "Maintenance").FirstOrDefault() });

                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
