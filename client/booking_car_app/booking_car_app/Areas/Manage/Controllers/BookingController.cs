using AutoMapper;
using booking_car_app.ApiServices.Booking;
using booking_car_app.ApiServices.Drivers;
using booking_car_app.Areas.Manage.Models;
using booking_car_app.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IDriverService driverService, IMapper mapper) 
        {
            _bookingService = bookingService;
            _driverService = driverService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookingService.GetAllBooking();
            return Json(new { data = JsonConvert.DeserializeObject<List<BookingInfo>>(result.Data.ToString()) });
        }
        [HttpGet]
        public async Task<IActionResult> GetHistory(Guid id)
        {
            var model = new BookingDetailViewModel();
            var respone = await _bookingService.GetBookingHistory(id);
            model.BookingHistories = JsonConvert.DeserializeObject<List<BookingHistory>>(respone.Data.ToString());            
            var result = await _bookingService.GetBookingById(id);
            var booking = JsonConvert.DeserializeObject<BookingInfo>(result.Data.ToString());
            if (!string.IsNullOrEmpty(booking.DriverId))
            {
                var response = await _driverService.GetById(Guid.Parse(booking.DriverId));
                model.Driver = JsonConvert.DeserializeObject<Entities.Driver>(response.Data.ToString());
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddBooking()
        {
            return View(new BookingViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking([FromBody] BookingViewModel model)
        {
            var bookingInfo = _mapper.Map<BookingInfo>(model);
            var result = await _bookingService.FindDriver(bookingInfo);
            return Json(new { result.Success, data = JsonConvert.SerializeObject(result.Data) });
        }
    }
}
