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
            return this.Ok();
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
}