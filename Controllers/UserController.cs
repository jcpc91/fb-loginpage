using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fb_loginpage.Services;
using fb_loginpage.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace fb_loginpage.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IConfiguration configuration;
        public UserController(IConfiguration _config ){
            this.configuration = _config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromForm]string token)
        {
            using(var fbservice = new FaceBookService(this.configuration)){
                Models.User me = await fbservice.GetMeAsync(token);
                var userservice = new UserService(this.configuration);
                string tokenapp = userservice.GenerateToken(me);
                return Json(tokenapp);
            }
        }
 
    }
}