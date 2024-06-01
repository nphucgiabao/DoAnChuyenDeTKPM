using AutoMapper;
using booking_car_app.ApiServices.Booking;
using booking_car_app.ApiServices.User;
using booking_car_app.Entities;
using booking_car_app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace booking_car_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServices _userServices;
        private readonly IBookingService _bookingServices;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IUserServices userServices, IBookingService bookingService, IMapper mapper)
        {
            _logger = logger;
            _userServices = userServices;
            _bookingServices = bookingService;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            //return View(new BookingInfo()
            //{
            //    UserId = User.FindFirst("Id")?.Value,
            //    Name = User.Identity.Name,
            //    Phone = User.FindFirst("UserName")?.Value
            //});
            if (role == "User")
            {
                return View(new BookingInfo()
                {
                    UserId = User.FindFirst("Id")?.Value,
                    Name = User.Identity.Name,
                    Phone = User.FindFirst("UserName")?.Value
                });
            }
            else if (role == "Driver")
            {
                return Redirect("/Driver/Booking/ReceiveNotifyBooking");
            }
            return Redirect("/Manage/Home/Index");
            // User.FindFirst("UserNme")?.Value });
        }
        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            //var result = await _userServices.GetUser();
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var user = _mapper.Map<Entities.User>(model);
                user.UserName = model.PhoneNumber;
                var result = await _userServices.Register(user);
                if (result.Success)
                    return RedirectToAction("Index");
                return View("Error", new ErrorViewModel { RequestId = result.Message });
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.ToString() });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.FindFirst("UserName")?.Value;
                var result = await _userServices.ResetPassword(username, model.Password);
                if (result.Success)
                {
                    return SignOut("Cookies", "oidc");
                }
                return View("Error", new ErrorViewModel { RequestId = result.Message });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Booking([FromBody]BookingInfo bookingInfo)
        {
            var result = await _bookingServices.FindDriver(bookingInfo);
            return Json(new { result.Success, data = JsonConvert.SerializeObject(result.Data)});
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPrice(int idType, decimal distance)
        {
            var result = await _bookingServices.UnitPrice(distance, idType);
            return Json(new { data = JsonConvert.SerializeObject(result.Data) });
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
        [HttpGet]
        public IActionResult Finish()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DonHang()
        {
            var result = await _bookingServices.GetBookingByUserId(User.FindFirst("Id")?.Value);
            var data = JsonConvert.DeserializeObject<List<BookingInfo>>(result.Data.ToString());
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
