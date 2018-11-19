using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace fb_loginpage.Controllers
{
    [Authorize(AuthenticationSchemes = 
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
             var resultBookList = new Book[] {
                new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451" },
                new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude" },
                new Book { Author = "George Orwell", Title = "1984" },
                new Book { Author = "Anais Nin", Title = "Delta of Venus" }
            };

            return Json(resultBookList);
        }

        [HttpGet("me")]
        public IActionResult GetMe(){
            
            var q  = this.User.Claims.Select(s => new {s.Type, s.Value});
            return this.Json(q);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return this.Ok();
        }
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class Book
    {
      public string Author { get; set; }
      public string Title { get; set; }
      public bool AgeRestriction { get; set; }
    }
}