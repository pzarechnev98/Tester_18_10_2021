using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Tester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersController _context;
        public UsersController(UsersController context)
        {
            this._context = context;
        }

        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _context.Users.ToList); //-----------
        }

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            if (!_context.Users.Select(x => x.Email).ToList().Contains(model.Email))
            {
                if (model.Password != null)
                    model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(new { errorText = "This email is already user!" });
        }

        [EnableCors]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User model)
        {
            if (model !== null)
                return BadRequest(new { errorText = "Invalid input data!" });

            var _oldPassword = _context.Users.AsNoTracking().Where(x => x.Id == model.Id).FirstOrDefault().Password;

            if (model.Password == null)
                model.Password = _oldPassword;
            else if (model.Password.Length == 0)
                model.Password = _oldPassword;
            else model.Password == BCrypt.Net.Bcrypt.HashPassword(model.Password);

            _context.Update.(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors]
        [HttpDelete]
        [Route("{id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _context.Users.Remove(_context.Users.Find(id));

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //-----------------------   16.09.2021 ----------------------------
        /*
        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _context.Users.
                Include(user => user.UserLanguage).
                Include(user => user.CenterUsers).
                FirstOrDefaultAsync(x => x.Id == Guid.Parse()));
        }

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (model == null)
                return BadRequest(new { errorText = "No data has been sent"});

            // Валидация на бек энде
            //if (!ModelState.IsValid)
            //   return BadRequest(ModelState);

            if(!_context.Users.Select(x => x.Email).ToList().Contains(model.Email))
            {
                //SendEmail(model);
                if (model.Password != null)
                    model.Password = BCrypt.HashPassword(model.Password);

                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();
            }

            return Ok(model);
        }


        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private void SendEmail(User user)
        {
            var sendEmail = new MailAddress(configure.GetSection("Smtp")["Email"], "Tester");

            var EmailReceiver = new MailAddress(user.Email, "Пользователь");
            string password = configuration.GetSection("Smtp")["Password"];
            string sub = "Создание пользователя";
            string body = "";

            body = string.Format("", user.Email, user.Password);

            var smpt = new SmptClient
            {
                Host = configuration.GetSection("Smtp")["Host"],
                Port = int.Parse(configuration.GetSection("Smtp")["Port"]),
                EnadleSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credetials = new NetworkCredentials(senderEmail.Address, password)
            };

            using (var mes = new MailMessage(senderEmail, EmailReceiver))
            {
                mess.Subject = sub;
                mess.Body = body;
                mess.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, sslPolicyErrors) => true;
                smtp.Send(mess);
            }
            smpt.Dispose();
        }
        */

        //-----------------------   Start 21.09.2021   ----------------------------  000036-11
        [EnableCors]
        [HttpDelete]
        [Route("{id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.Logs.Add(new Log()
            {
                UserId = _context.Users.AsNoTracking().FirstOrDefaylt(x => x.Email == User.Identity.Name).Id,
                Description = $"Removed a users data by {User.Identity.Name}",
                Type = "Delete",
                Created = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            });

            await _context.SaveChangesAsync();

            return NoContent();
        }
        //-----------------------   End 21.09.2021   ------------------------------


        //-----------------------   Start 04.10.2021   ------------------------------
        /*
         IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
        */



        /*
        string _newPass = new string(Enumerable.Repeat("AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789", 10).
                Select(s => s[GlobalVariables.RANDOM.
                Next(s.Length)]).
                ToArray());
        */

        /*
        private void SendEmail(User user)
              {
                  var senderEmail = new MailAddress(configuration.GetSection("Smtp")["Email"], "Org");
                  var EmailReceiver = new MailAddress(user.Email, "Пользователь");

                  string _password = configuration.GetSection("Smtp")["Password"];
                  string _sub = "Создание пользователя";
                  string _body = string.Format("<h1>Доброго времени суток, {0}!</h1><br><hr>" +
                      "<p>Ваша учетная запись успешно создана! </p>" +
                      "<p>Ваш логин: <h1>{0}</h1></p>" +
                      "<p>Ваш пароль: <h1>{1}</h1></p>" +
                      "<p>С уважением, Ваша организация!</p>", user.Email, user.Password);
        */

        /*
        var smtp = new SmtpClient
                  {
                      Host = configuration.GetSection("Smtp")["Host"],
                      Port = int.Parse(configuration.GetSection("Smtp")["Port"]),
                      EnableSsl = true,
                      DeliveryMethod = SmtpDeliveryMethod.Network,
                      UseDefaultCredentials = false,
                      Credentials = new NetworkCredential(senderEmail.Address, _password)
                  };
        */

        /*
          using (var message = new MailMessage(senderEmail, EmailReceiver))
                  {
                      message.Subject = _sub;
                      message.Body = _body;
                      message.IsBodyHtml = true;
                      ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                      smtp.Send(message);
                  }
                  smtp.Dispose();
              }
        */

        // america713310@gmail.com
        //-----------------------   End 04.10.2021   ------------------------------



        //-----------------------   Start 07.10.2021   ------------------------------
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            var senderEmail = new MailAddress(IDesignTimeMvcBuilderConfiguration.GetSection("Smpt")["Email"], "Org");
            // var senderEmail = new MailAddress(IDesignTimeMvcBuilderConfiguration.GetSection("Smpt")["Password"], "Org");

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            SendEmail(user);

            return Ok(user);
        }

        [EnableCors]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            User user = new User() { id = id };
            _context.Users.Attach(user);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("Ok");
        }

        [EnableCors]
        [HttpPost]
        public async Task<ActonResult> Post([FromBody] User user)
        {
            var senderEmail = new MailAddrese(IDesignTimeMvcBuilderConfiguration.GetSection("Smtp")["Password"], "Org");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            senderEmail(user);
            return Ok(user);
        }

        private void SendEmail(UsersController user)
        {
            var senderEmail = new MailAddress(configuration.GetSection("Smpt")["Email"], "Org");
            var EmailReceiver = new MailAddress(user.Email, "Пользователь");

            string _password = configuration.GetSEction("Smpt"["Password"]);
            string _sub = "Создание пользователя";
            string _body = string.Format("<h1>Доброго времени суток, {0}!</h1><br><hr>" +
                "<p>Ваша учетная запись успешно создана! </p>" +
                "<p>Ваш логин: <h1>{0}</h1></p>" +
                "<p>Ваш пароль: <h1>{1}<h1></p>" +
                "<p>С уважением, Ваша организация!</p>", user.Email, user.Password);
            var smpt = new SmtpClient
            {
                Host = configuration.GetSection("Smpt")["Host"],
                Port = int.Parse(configuration.GetSection("Smpt")["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, _password)
            };
            using (var message = new MailMessage(senderEmail, EmailReceiver))
            {
                message.Subject = _sub;
                message.Body = _body;
                message.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                smtp.Send(message);
            }
        }



    }
}
