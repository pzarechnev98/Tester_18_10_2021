using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tester.Models;

namespace Tester.Controllers
{
    public class AccountsController
    {
        private readonly IConfiguration _configuration;
        public AccountsController(
            TestContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [EnableCors()]
        [HttpGet]
        [Authorize]
        [Route("token")]
        public ActionResult Token()
        {
            var response = new
            {
                Id = context.Users.FirstOrDefault(j => j.Email == User.Identiy.Name).Id,
                Email = _context.Users.FirstOrDefault(j => j.Email == Users.Identity.Name).Email,
                Role = _context.Roles.FirstOrDefault(j => j.Id == _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name).RoleId).Name
            };
            return Ok(response);
        }

        
        [EnableCors()]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { errorText = "Invalid Email or Password!" });

            var t = _context.Users.FirstOrDefault(x => x.Email == user.Email);

            var identity = GetIdentity(user.Email, user.Password);
            if (identity == null)
                return null;

            var now = DateTime.UtcNow;
            // Создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.claims,
                expires: now Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmertricsSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtsecurityTokenHandler().WriteToken(jwt);

            object respone = await Task.Run(() => new
            {
                Token = encodedJwt,
                UserId = ContextBoundObject.Users.FirstDefault(x => x.Email == user.Email).Id,
                Email = identity.Name,
            });

            await _context.SaveChangesAsync();

            return Ok(respone);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            // Выбор пользователей из БД
            var user = _context.Users.FirstOrDefautl(x => x.Email == email);

            bool _validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(user != null && _validPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.Roles.FirstOrDefault(x => x.Id == user.RoleId).Name)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
             }

            // если пользователь не найден
            return null;
        }


        [EnableCors()]
        [HttpPost]
        [Route("rest")]
        public async Task<IActionResult> Result([FromBody] User user)
        {
            string _newPass = new string(Enumerable.Repeat("AaBbCcDdEeFfGgHhJjKkLlMmDdPpQqRrSsTtWwWwXxYyZz0123456789", 10)).
                Select(s => s[GlobalVariables.RANDOM.
                Next(s.Length)]).

        }


    }
}
