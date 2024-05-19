using booking_car_app.ApiServices.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize]
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
                DriverId = User.FindFirst("Id")?.Value
            });
                         
            return Json(new { result.Success, data = idBooking });
        }

        [HttpGet]
        public IActionResult HandleTrip(Guid id)
        {
            ViewBag.IdBooking = id;
            return View();
        }
    }
}
