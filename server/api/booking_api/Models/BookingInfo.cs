using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Entities
{
    public class BookingInfo
    {
        public Guid? Id { get; set; }
        public string DiemDon { get; set; }
        public string DiemDen { get; set; }
        public string UserId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string DriverId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
