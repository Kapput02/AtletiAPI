using AtletiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AtletiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult Login([FromBody] User credentials) 
        {
            using(OlympicsContext model = new OlympicsContext())
            {
                User candidate = model.Users.FirstOrDefault(q => q.Username == credentials.Username && q.Password == credentials.Password);
                
                if (candidate == null) return Unauthorized();

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    SigningCredentials = new SigningCredentials(SecurityKeyGenerator.GetSecurityKey(), SecurityAlgorithms.HmacSha256),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Subject = new ClaimsIdentity
                    (
                        new Claim[]
                        {
                            new Claim("id",candidate.Username)
                        }
                    )
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
        }
        [Authorize]
        [HttpPost("/api/Logout")]
        public ActionResult Logout()
        {
            return Ok();
        }
    }
}
