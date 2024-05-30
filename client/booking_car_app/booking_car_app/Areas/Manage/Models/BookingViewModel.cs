using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Manage.Models
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Điểm đón chưa được nhập!")]
        public string DiemDon { get; set; }
        [Required(ErrorMessage = "Điểm đến chưa được nhập!")]
        public string DiemDen { get; set; }
        [Required(ErrorMessage = "Tên khách hàng chưa được nhập!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Số điện thoại chưa được nhập!")]
        public string Phone { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
