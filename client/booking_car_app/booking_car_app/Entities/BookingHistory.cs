﻿using System;

namespace booking_car_app.Entities
{
    public class BookingHistory
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
    }
}
