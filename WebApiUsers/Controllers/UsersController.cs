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
                // Fill departments
                Department technical = new Department { Name = "Technical" };
                Department marketing = new Department { Name = "Marketing" };
                Department IT = new Department { Name = "IT" };
                Department bookkering = new Department { Name = "Bookkering" };
                Department legal = new Department { Name = "Legal" };
                Department maintenance = new Department { Name = "Maintenance" };

                dbDeps.AddRange(technical, marketing, IT, bookkering, legal, maintenance);
                dbDeps.SaveChanges();

                // Fill users
                db.Users.Add(new User { Name = "Andrew Thomas", Department = technical });
                db.Users.Add(new User { Name = "Bill Moore", Department = technical });
                db.Users.Add(new User { Name = "Keiran Moyer", Department = marketing });
                db.Users.Add(new User { Name = "Isobella Walmsley", Department = marketing });
                db.Users.Add(new User { Name = "Miruna Bullock", Department = IT });
                db.Users.Add(new User { Name = "Kier Almond", Department = IT });
                db.Users.Add(new User { Name = "Huzaifa Petersen", Department = bookkering });
                db.Users.Add(new User { Name = "Akash Roberts", Department = bookkering });
                db.Users.Add(new User { Name = "Gino Zhang", Department = legal });
                db.Users.Add(new User { Name = "Kya Mcpherson", Department = legal });
                db.Users.Add(new User { Name = "Olli Conroy", Department = maintenance });
                db.Users.Add(new User { Name = "Fynn Orozco", Department = maintenance });

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
