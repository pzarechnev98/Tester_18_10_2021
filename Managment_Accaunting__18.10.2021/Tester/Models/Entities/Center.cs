using System;
using System.Collections.Generic;

namespace Tester.Models.Entities
{
    public class Center
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int cityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string Schedule { get; set; }
        // Номера телефонов центра
        public int Phone { get; set; }
        // Ссылка на соц.сеть
        public string Social { get; set; }
        public double? Royalti { get; set; }
        // Взнос
        public int? Lump { get; set; }
        // Площадь центра
        public string Square { get; set; }
        // Платеж
        public int? Revenue { get; set; }
        // Задолжность
        public int? Debt { get; set; }
        // Посещаемость
        public int? Visites { get; set; }
        // Оплаченные
        public int? Paid { get; set; }
        public DateTime Created { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public List<Lead> Leads { get; set; }
        public List<GroupEntity> GroupEntities { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Student> Students { get; set; }
        public List<CenterUser> CenterUsers { get; set; }
        
    }
}
