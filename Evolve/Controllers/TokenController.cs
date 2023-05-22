using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InventoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration config;
        //public readonly AppDbContext context;
        public TokenController(IConfiguration config)
        {
            this.config = config;
            //this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInf userInfo)
        {
            if (userInfo != null && userInfo.UserName != null && userInfo.Password !=null)
            {
                var user = await GetUser(userInfo.UserName, userInfo.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Password", user.Password)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            config["Jwt:Issuer"],
                            config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(20),
                            signingCredentials: signIn
                        );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Creadentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<UserInf> GetUser(string userName, string pass)
        {
            return await context.Users.FirstOrDefaultAsync(u=>u.UserName ==userName && u.Password==pass);
        }

    }
}
