using booking_api.Infrastructure.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories.Bookings
{
    public class BookingRepository : RepositoryBase<car_bookingContext, Booking>, IBookingRepository
    {
        public BookingRepository(car_bookingContext context) : base(context) { }
    }
}
