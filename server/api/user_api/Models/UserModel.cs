using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Models
{
    public class UserModel
    {
        public string FullName { get; set; }
        //public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        public string Role { get; set; }
        public string OId { get; set; }
    }
}
