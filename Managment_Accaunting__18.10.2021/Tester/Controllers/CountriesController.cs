using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Controllers
{
    public class CountriesController
    {
        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _context.Logs.Add(new Log()
            {
                UserId = _context.Users.AsNoTracking().FirstOrDefault(x => x.Email == UsersController.Identity.Name).id,
                Description = $"Getting a country IDd by {User.Identity.Name}",
                Type ="Get",
                Created = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            });

            await _context.SaveChangesAsync();

            return Ok(await _rep.GetById(id));
        }


    }
}
