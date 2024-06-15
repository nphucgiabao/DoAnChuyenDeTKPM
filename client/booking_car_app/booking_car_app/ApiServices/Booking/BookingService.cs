using booking_car_app.Entities;
using booking_car_app.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.Booking
{
    public class BookingService : ServiceCore, IBookingService
    {
        public BookingService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, httpContextAccessor){ }

        public async Task<ResponseModel> CancelBooking(Guid id)
        {
            return await DeleteRequest($"/user/CancelBooking/{id}");
        }

        public async Task<ResponseModel> FindDriver(BookingInfo bookingInfo)
        {
            return await PostRequest<BookingInfo>("/user/booking", bookingInfo);
        }

        public async Task<ResponseModel> GetAllBooking()
        {
            return await GetRequest("/manage/getAllBooking");
        }

        public async Task<ResponseModel> GetBookingByDriverId(string id)
        {
            return await GetRequest($"/driver/getBookingByDriver/{id}");
        }

        public async Task<ResponseModel> GetBookingById(Guid id)
        {
            return await GetRequest($"/user/getBookingById/{id}");
        }

        public async Task<ResponseModel> GetBookingByUserId(string id)
        {
            return await GetRequest($"/user/getBookingByUser/{id}");
        }

        public async Task<ResponseModel> GetBookingHistory(Guid id)
        {
            return await GetRequest($"/manage/getHistoryBooking/{id}");
        }

        public async Task<ResponseModel> ReceiveBooking(BookingInfo bookingInfo)
        {
            return await PostRequest<BookingInfo>("/user/receivebooking", bookingInfo);
        }

        public async Task<ResponseModel> UnitPrice(decimal distance, int typeId)
        {
            var formattedDecimal = distance.ToString(CultureInfo.InvariantCulture);
            return await GetRequest($"/user/unitPrice/{formattedDecimal}/{typeId}");
        }

        public async Task<ResponseModel> UpdateStatusBooking(BookingInfo bookingInfo)
        {
            return await PostRequest<BookingInfo>("/driver/updateStatusBooking", bookingInfo);
        }
    }
}
