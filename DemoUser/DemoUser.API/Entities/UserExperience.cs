using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.API.Entities
{
    public class UserExperience
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public int UserId { get; set; } 

    }
}
