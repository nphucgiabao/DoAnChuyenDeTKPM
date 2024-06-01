using booking_car_app.ApiServices.Booking;
using booking_car_app.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Driver.Controllers
{
    [Area("Driver")]
    //[Authorize(Roles = "Driver")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingServices;
        public BookingController(IBookingService bookingService)
        {
            _bookingServices = bookingService;
        }

        public IActionResult ReceiveNotifyBooking()
        {
            ViewBag.Name = User.Identity.Name;
            var driverId = User.FindFirst("OId")?.Value;
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ReceiveBooking(Guid idBooking)
        //{
        //    var result = await _bookingServices.ReceiveBooking(new Entities.BookingInfo()
        //    {
        //        Id = idBooking,
        //        DriverId = User.FindFirst("OId")?.Value,
        //        Status = 2
        //    });                         
        //    return Json(new { result.Success, data = idBooking, result.Message });
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatusBooking(Guid idBooking, int status)
        {
            var result = await _bookingServices.UpdateStatusBooking(new Entities.BookingInfo()
            {
                Id = idBooking,
                DriverId = User.FindFirst("OId")?.Value,
                Status = status
            });
            return Json(new { result.Success, data = idBooking, result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> HandleTrip(Guid id)
        {
            //ViewBag.IdBooking = id;
            var booking = await _bookingServices.GetBookingById(id);
            var data = booking.Data.ToString();
            return View(JsonConvert.DeserializeObject<BookingInfo>(data));
        }
        [HttpGet]
        public IActionResult Finish()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DonHang()
        {
            var result = await _bookingServices.GetBookingByDriverId(User.FindFirst("OId")?.Value);
            var data = JsonConvert.DeserializeObject<List<BookingInfo>>(result.Data.ToString());
            return View(data);
        }
    }
}
