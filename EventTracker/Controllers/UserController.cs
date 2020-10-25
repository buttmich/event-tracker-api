using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private EventsContext _db;

        public UserController(EventsContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(string username)
        {
            var user = _db.Users.Where(user => user.Username == username).ToList().FirstOrDefault();
            if (user != null)
            {
                return Ok(user);
            }
            return StatusCode(400);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Users newUser)
        {
            var existingUser = _db.Users.Where(user => user.Username == newUser.Username).ToList();
            if(existingUser.Count == 0)
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok();
            }
            return StatusCode(400);
        }
    } 
}
