using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fb_loginpage.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace fb_loginpage.Services
{
    public class UserService
    {
        readonly IConfiguration configure;
        public UserService(IConfiguration _config){
            this.configure = _config;
        }

        //Genera el token a partir del usuario autenticado en facebook
        public string GenerateToken(User user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configure["jwt:security_key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configure["jwt:authority"],
                //Audience = configure["jwt:audience"],
                Expires = DateTime.Now.AddDays(10),
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Authentication, "fb"),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("fb", user.Id)
                }),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}