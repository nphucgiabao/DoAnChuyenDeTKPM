using booking_car_app.ApiServices.Booking;
using booking_car_app.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(JwtBearerDefaults.AuthenticationScheme, Roles = "Driver")]
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceiveBooking(Guid idBooking)
        {
            var result = await _bookingServices.ReceiveBooking(new Entities.BookingInfo()
            {
                Id = idBooking,
                DriverId = User.FindFirst("OId")?.Value
            });                         
            return Json(new { result.Success, data = idBooking });
        }

        [HttpGet]
        public async Task<IActionResult> HandleTrip(Guid id)
        {
            //ViewBag.IdBooking = id;
            var booking = await _bookingServices.GetBookingById(id);
            var data = booking.Data.ToString();
            return View(JsonConvert.DeserializeObject<BookingInfo>(data));
        }
    }
}
