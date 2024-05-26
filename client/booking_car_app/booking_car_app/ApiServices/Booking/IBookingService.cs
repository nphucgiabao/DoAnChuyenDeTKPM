using booking_car_app.Entities;
using booking_car_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.Booking
{
    public interface IBookingService
    {
        Task<ResponseModel> FindDriver(BookingInfo bookingInfo);
        Task<ResponseModel> ReceiveBooking(BookingInfo bookingInfo);
        Task<ResponseModel> UnitPrice(decimal distance, int typeId);
        Task<ResponseModel> GetBookingById(Guid id);
    }
}
