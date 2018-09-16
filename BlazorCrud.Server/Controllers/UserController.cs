using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserContext _context;

        public UserController(IConfiguration config, UserContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("logon")]
        public IActionResult LogOn([FromBody]Login login)
        {
            IActionResult response = Unauthorized();
            var user = _context.Users
                .Where(u => u.Username == login.Username && u.Password == login.Password)
                .FirstOrDefault();
            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private string BuildToken(User u)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, u.Username),
                new Claim(JwtRegisteredClaimNames.GivenName, u.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, u.LastName),
                new Claim(JwtRegisteredClaimNames.Email, u.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireTime"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}