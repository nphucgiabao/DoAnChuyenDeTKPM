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
        public IActionResult ReceiveNotifyBooking()
        {
            return View();
        }
    }
}
