using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using fb_loginpage.Services;

namespace fb_loginpage.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
        readonly IConfiguration configuration;
        public FacebookController(IConfiguration _config ){
            this.configuration = _config;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Get(string token)
        {
            using(var service = new FaceBookService(this.configuration)){
                var result = await service.GetMeAsync(token);
                return Json(result);
            }
        }
        
    }
}