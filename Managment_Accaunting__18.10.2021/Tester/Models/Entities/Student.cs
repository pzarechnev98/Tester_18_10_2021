using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public int Age { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public Center Center { get; set; }
        public int? SourceId { get; set; }
        public Source Source { get; set; }
        public string SchoolName { get; set; }
        public double Balance { get; set; }
        public string Grade { get; set; }
        public string ParentOneFirstName { get; set; }
        public string ParentOneLastName { get; set; }
        public string ParentOnePhone { get; set; }
        public string ParentOneEmail { get; set; }
        public string ParentTwoFirstName { get; set; }
        public string ParentTwoLastName { get; set; }
        public string ParentTwoPhone { get; set; }
        public string ParentTwoEmail { get; set; }
        public DateTime Created { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public List<StudentGroup> StudentGroups { get; set; }

    }
}
