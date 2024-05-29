using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace booking_car_app.Areas.Manage.Models
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Username không được trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password không được trống")]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid OId { get; set; }
    }
}
