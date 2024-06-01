﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Features.Bookings.Models.Request
{
    public class BookingModelRequest
    {
        public Guid? Id { get; set; }
        public string UserId { get; set; }
        public string DiemDon { get; set; }
        public string DiemDen { get; set; }
        public int Status { get; set; }
        public DateTime NgayTao { get; set; }
        public string DriverId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
