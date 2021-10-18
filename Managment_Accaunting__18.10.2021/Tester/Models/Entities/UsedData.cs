using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Models.Entities
{
    public class UsedData
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now.Date;
    }
}

// 2021-01-01
// 2021:01:01
// 20210/01/01
// "01-01-2021"
// 01.Feb.2021
