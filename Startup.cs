using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace fb_loginpage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config => {
                config.RequireHttpsMetadata = false;
                config.Audience = Configuration["jwt:audience"];
                //config.Authority = Configuration["jwt:authority"];
                //config.ClaimsIssuer = Configuration["jwt:authority"];
                config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters(){
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["jwt:security_key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer =  Configuration["jwt:authority"],
                    ValidateAudience = false,
                    //ValidAudience = Configuration["jwt:audience"],
                    ValidateLifetime = true,
                    
                };
            });

            // services.AddAuthorization(opt => {
            //     opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            //     .RequireAuthenticatedUser()
            //     .Build();
                
            // });

            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
