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
    public class EventController : ControllerBase
    {
        private EventsContext _db;

        public EventController(EventsContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            var user = _db.Users.Where(user => user.Username == username).ToList().FirstOrDefault();
            if (user != null)
            {
                var categoryIDs = _db.Categories.Where(category => category.UserId == user.Id).Select(x => x.Id).ToList();
                var events = _db.Events.Where(e => categoryIDs.Contains(e.CategoryId)).ToList();
                return Ok(events);
            }
            return StatusCode(400);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Events newEvent)
        {
            try
            {
                _db.Events.Add(newEvent);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
