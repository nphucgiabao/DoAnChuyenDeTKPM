using booking_car_app.Entities;
using booking_car_app.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.Booking
{
    public class BookingService : ServiceCore, IBookingService
    {
        public BookingService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, httpContextAccessor){ }
        public async Task<ResponseModel> FindDriver(BookingInfo bookingInfo)
        {
            return await PostRequest<BookingInfo>("/user/booking", bookingInfo);
        }

        public async Task<ResponseModel> ReceiveBooking(BookingInfo bookingInfo)
        {
            return await PostRequest<BookingInfo>("/user/receivebooking", bookingInfo);
        }
    }
}
