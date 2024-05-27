using System;
using System.Collections.Generic;

namespace booking_api.Infrastructure.Repository.Entities
{
    public partial class Booking
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DiemDon { get; set; }
        public string DiemDen { get; set; }
        public int Status { get; set; }
        public DateTime NgayTao { get; set; }
        public string DriverId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
