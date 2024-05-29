using booking_api.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories.BookingHistories
{
    public class BookingHistoryRepository : RepositoryBase<car_bookingContext, BookingHistory>, IBookingHistoryRepository
    {
        public BookingHistoryRepository(car_bookingContext context) : base(context) { }
    }
}
