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
    public class CategoryController : ControllerBase
    {
        private EventsContext _db;

        public CategoryController(EventsContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            var user = _db.Users.Where(user => user.Username == username).ToList().FirstOrDefault();
            if (user != null)
            {
                var categories = _db.Categories.Where(category => category.UserId == user.Id).ToList();
                return Ok(categories);
            }
            return StatusCode(400);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categories newCategory)
        {
            try
            {
                _db.Categories.Add(newCategory);
                _db.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
