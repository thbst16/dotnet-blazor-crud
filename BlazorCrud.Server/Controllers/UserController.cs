using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config, UserContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of paginated users with a default page size of 10.
        /// </summary>
        [HttpGet]
        public PagedResult<User> GetAll([FromQuery]int page)
        {
            int pageSize = 10;
            // Do not send password over webAPI GET
            foreach (User u in _context.Users)
            {
                u.Password = string.Empty;
            }
            return _context.Users
                .OrderBy(p => p.Id)
                .AsNoTracking()
                .GetPaged(page, pageSize);
        }

        /// <summary>
        /// Gets a specific user by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            var item = _context.Users.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            // Do not send password over webAPI GET
            item.Password = string.Empty;
            return item;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtRoute("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Updates a user with a specific Id.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var usr = _context.Users.Find(id);
                if (usr == null)
                {
                    return NotFound();
                }

                _mapper.Map(user, usr);
                _context.Users.Update(usr);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deletes a specific user by Id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Allows a user to authenticate and receive a JWT token for API calls.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("logon")]
        public IActionResult LogOn([FromBody]Login login)
        {
            // Removed unauthorized default since Blazor can't handle HTTP 403 respons
            // IActionResult response = Unauthorized();
            IActionResult response = Ok(new { token = string.Empty });
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
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, u.Username),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.GivenName, u.FirstName),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.FamilyName, u.LastName),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, u.Email)
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