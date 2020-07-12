using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using WebApiDepartments.Models;

namespace WebApiDepartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UsersContext db;

        public UsersController(UsersContext context)
        {
            db = context;

            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Andrew Thomas", Department = new Department() { Name = "Technical" } });
                db.Users.Add(new User { Name = "Bill Moore", Department = new Department() { Name = "Technical" } });
                db.Users.Add(new User { Name = "Keiran Moyer", Department = new Department() { Name = "Technical" } });
                db.Users.Add(new User { Name = "Isobella Walmsley", Department = new Department() { Name = "Marketing" } });
                db.Users.Add(new User { Name = "Miruna Bullock", Department = new Department() { Name = "Marketing" } });
                db.Users.Add(new User { Name = "Kier Almond", Department = new Department() { Name = "IT" } });
                db.Users.Add(new User { Name = "Huzaifa Petersen", Department = new Department() { Name = "IT" } });
                db.Users.Add(new User { Name = "Akash Roberts", Department = new Department() { Name = "IT" } });
                db.Users.Add(new User { Name = "Gino Zhang", Department = new Department() { Name = "IT" } });
                db.Users.Add(new User { Name = "Kya Mcpherson", Department = new Department() { Name = "Bookkering" } });
                db.Users.Add(new User { Name = "Olli Conroy", Department = new Department() { Name = "Bookkering" } });
                db.Users.Add(new User { Name = "Fynn Orozco", Department = new Department() { Name = "Bookkering" } });
                db.Users.Add(new User { Name = "Barney Beaumont", Department = new Department() { Name = "Legal" } });
                db.Users.Add(new User { Name = "Roscoe Cline", Department = new Department() { Name = "Legal" } });
                db.Users.Add(new User { Name = "Beatriz O'Reilly", Department = new Department() { Name = "Legal" } });
                db.Users.Add(new User { Name = "Danial Hollis", Department = new Department() { Name = "Legal" } });
                db.Users.Add(new User { Name = "Lacie Mill", Department = new Department() { Name = "Maintenance" } });
                db.Users.Add(new User { Name = "Dainton Whitmore", Department = new Department() { Name = "Maintenance" } });
                db.Users.Add(new User { Name = "Jesse Cotton", Department = new Department() { Name = "Maintenance" } });
                db.Users.Add(new User { Name = "Jemma Battle", Department = new Department() { Name = "Maintenance" } });
                db.Users.Add(new User { Name = "Reiss Perkins", Department = new Department() { Name = "Maintenance" } });
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
