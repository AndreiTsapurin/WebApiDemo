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
        public UsersController(UsersContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Andrew Thomas", DepId = 1 });
                db.Users.Add(new User { Name = "Bill Moore", DepId = 1 });
                db.Users.Add(new User { Name = "Keiran Moyer", DepId = 1 });
                db.Users.Add(new User { Name = "Isobella Walmsley", DepId = 2 });
                db.Users.Add(new User { Name = "Miruna Bullock", DepId = 2 });
                db.Users.Add(new User { Name = "Kier Almond", DepId = 2 });
                db.Users.Add(new User { Name = "Huzaifa Petersen", DepId = 2 });
                db.Users.Add(new User { Name = "Akash Roberts", DepId = 3 });
                db.Users.Add(new User { Name = "Gino Zhang", DepId = 3 });
                db.Users.Add(new User { Name = "Kya Mcpherson", DepId = 4 });
                db.Users.Add(new User { Name = "Olli Conroy", DepId = 4 });
                db.Users.Add(new User { Name = "Fynn Orozco", DepId = 4 });
                db.Users.Add(new User { Name = "Barney Beaumont", DepId = 4 });
                db.Users.Add(new User { Name = "Roscoe Cline", DepId = 5 });
                db.Users.Add(new User { Name = "Beatriz O'Reilly", DepId = 5 });
                db.Users.Add(new User { Name = "Danial Hollis", DepId = 5 });
                db.Users.Add(new User { Name = "Lacie Mill", DepId = 5 });
                db.Users.Add(new User { Name = "Dainton Whitmore", DepId = 6 });
                db.Users.Add(new User { Name = "Jesse Cotton", DepId = 6 });
                db.Users.Add(new User { Name = "Jemma Battle", DepId = 6 });
                db.Users.Add(new User { Name = "Reiss Perkins", DepId = 6 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            List<User> users = await db.Users.ToListAsync();

            //string link = "https://localhost:44330/api/departments";
            string link = "https://217.76.39.39:25720/Warehouses/GetSaldoByIds?token=PbLuroWxX9Q9lOC7VI3V&requestid=fbbe2b5fd9a94a9750d2872878fdaae2&warehouses=RG008SB9RFH1KR&products=RQLAGL2GL5RSDR,RG008P0RNAEHVS";

            //WebClient wc = new WebClient();
            //wc.

            string response = new WebClient().DownloadString(link);

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
