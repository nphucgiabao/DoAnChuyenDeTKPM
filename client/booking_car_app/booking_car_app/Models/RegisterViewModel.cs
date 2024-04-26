using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Họ và tên chưa được nhập.")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Mật khẩu chưa được nhập.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Số di động chưa được nhập.")]
        [Display(Name = "Số di động")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Số di động không hợp lệ.")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
