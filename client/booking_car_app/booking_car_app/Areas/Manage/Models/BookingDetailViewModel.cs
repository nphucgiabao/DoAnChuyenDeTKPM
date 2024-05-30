using booking_car_app.Entities;
using System.Collections.Generic;

namespace booking_car_app.Areas.Manage.Models
{
    public class BookingDetailViewModel
    {
        public Entities.Driver Driver { get; set; }
        public List<BookingHistory> BookingHistories { get; set; }
    }
}
