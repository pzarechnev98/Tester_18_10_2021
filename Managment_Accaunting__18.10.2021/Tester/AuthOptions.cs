using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Token;
using System.Text;

namespace Tester
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1140; // время жизни токена - 1140 минут (24 часа)
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

    [EnableCors()]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        if (user == null)
            return BadRequest(new { errorText = "No data has been sent!" });

        var identity = GetIdentity(user.Email, user.Password);

        if (identity == null)
            return BadRequest(new { errorText = "Invalid Email or Password!" });

        // Создание JWT-токена
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.Now,
                claims: identity.Claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = await Task.Run(() => new
        {
            Token = encodedJwt,
            UserId = _context.Users.FirstOrDefault(x => x.Email == user.Email).Id,
            Email = identity.Name,
            Role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault(),
            Centers = _context.Centers
                .Where(x => _context.CenterUsers
                .Where(x => x.UserId == _context.Users
                .FirstOrDefault(x => x.Email == user.Email).Id)
                .Select(x => x.CenterId)
                .Contains(x.Id))
                .Select(x => new { x.Id, x.Name })
        });

        _context.Users.FirstOrDefault(x => x.Email == user.Email).LastEntry = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        _context.Logs.Add(new Log()
        {
            UserId = response.UserId,
            Description = $"Logged in as {response.Email}",
            Type = "Post",
            Created = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        });

        await _context.SaveChangesAsync();

        return Ok(response);
    }
    private ClaimsIdentity GetIdentity(string email, string password)
    {
        // Проверка на наличие пользователя с такой почтой
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        if (user == null)
            return null;

        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
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

        return null;
    }


    //------------------------------------------------------

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1140; // время жизни токена - 1140 минут (24 часа)
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }


    [EnableCors()]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        if (user == null)
            return BadRequest(new { errorText = "No data has been sent!" });

        var identity = GetIdentity(user.Email, user.Password);

        if (identity == null)
            return BadRequest(new { errorText = "Invalid Email or Password!" });

        // Создание JWT-токена
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.Now,
                claims: identity.Claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = await Task.Run(() => new
        {
            Token = encodedJwt,
            UserId = _context.Users.FirstOrDefault(x => x.Email == user.Email).Id,
            Email = identity.Name,
            Role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault(),
            Centers = _context.Centers
                .Where(x => _context.CenterUsers
                .Where(x => x.UserId == _context.Users
                .FirstOrDefault(x => x.Email == user.Email).Id)
                .Select(x => x.CenterId)
                .Contains(x.Id))
                .Select(x => new { x.Id, x.Name })
        });

        _context.Users.FirstOrDefault(x => x.Email == user.Email).LastEntry = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        _context.Logs.Add(new Log()
        {
            UserId = response.UserId,
            Description = $"Logged in as {response.Email}",
            Type = "Post",
            Created = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        });

        await _context.SaveChangesAsync();

        return Ok(response);
    }
    private ClaimsIdentity GetIdentity(string email, string password)
    {
        // Проверка на наличие пользователя с такой почтой
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        if (user == null)
            return null;

        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
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

        return null;
    }



    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // укзывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,

            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,

            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });



}
