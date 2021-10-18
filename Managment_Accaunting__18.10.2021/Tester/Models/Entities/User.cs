using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Models.Entities
{
    public class User
    {
        //--------------
        //--------------------------- Migrations
        //add-migration AddedUser
        //add-migration RemovedUser
        //update-database
        //--------------
        [Key]
        public Guid Id { get; set; }
        // [Required(ErrorMessage = "Укажите имя пользователя")]
        //[StringLength(20, ErrorMessage = "Длина username должна быть от 8 до 20 символов", MinimumLength = 8)]
        // public string Username { get; set; }

        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        // [Required(ErrorMessage = "Укажите почту")]
        // [DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public string Email { get; set; }
        // [Required(ErrorMessage = "Укажите пароль")]
        // [StringLength(20, ErrorMessage = "Длина пароля должна быть от 8 до 20 символов", MinimumLength = 8)]
        // [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string phone { get; set; }
        public string IIN { get; set; }
        public string Address { get; set; }
        //public DateTime? Created { get; set; } = DateTime.Now.Date;
        

        public string CheckNumber { get; set; }
        public DateTime? Created { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        
        public List<CenterUser> CenterUsers { get; set; }
        public List<UserLanguage> UserLanguages { get; set; }

    }
}
