using booking_api.Entities;
using booking_api.Hubs;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> _broadcastHub;
        private readonly IBookingRepository _bookingRepository;
        public BookingController(IHubContext<BroadcastHub> broadcastHub, IBookingRepository bookingRepository)
        {
            _broadcastHub = broadcastHub;
            _bookingRepository = bookingRepository;
        }        
        [HttpPost]
        public async Task<IActionResult> FindDriver([FromBody] BookingInfo info)
        {
            //RabbitMQPublisher<BookingInfo> rabbitMQPublisher = new RabbitMQPublisher<BookingInfo>("booking");
            //rabbitMQPublisher.SendMessage(info).Start();
            var data = JsonConvert.SerializeObject(info);
            var booking = new Booking();
            booking.Id = Guid.NewGuid();
            booking.DiemDon = info.DiemDon;
            booking.DiemDen = info.DiemDen;
            booking.NgayTao = DateTime.Now;
            booking.Status = 1;
            _bookingRepository.Insert(booking);
            await _broadcastHub.Clients.All.SendAsync("BroadcastBooking", data);
            
            return Ok();
        }
    }
}
