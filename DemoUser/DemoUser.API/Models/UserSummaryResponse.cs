using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.API.Models
{
    public class UserSummaryResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
