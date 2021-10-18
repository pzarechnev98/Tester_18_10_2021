using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Models.Entities
{
    public class StudentGroup
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Discount Discount { get; set; }
        public StudentStatusEnums StudentsStatusEnum { get; set } = StudentStatusEnum.Studying;
    }
}
