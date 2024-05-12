using System;
using System.Collections.Generic;

namespace booking_api.Data
{
    public partial class BookingHistory
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
    }
}
