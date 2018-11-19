using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fb_loginpage.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;

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
            #region  https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Authentication, JwtBearerDefaults.AuthenticationScheme),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                };
            var useridentiy = new ClaimsIdentity(claims, "Facebook");
            
            #endregion
            //http://bitoftech.net/2014/10/27/json-web-token-asp-net-web-api-2-jwt-owin-authorization-server/
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configure["jwt:security_key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configure["jwt:audience"],
                Issuer = configure["jwt:authority"],
                //Audience = configure["jwt:audience"],
                Expires = DateTime.Now.AddDays(10),
                Subject = useridentiy,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // public string CreateRefreshToken() {

        // }
    }
}