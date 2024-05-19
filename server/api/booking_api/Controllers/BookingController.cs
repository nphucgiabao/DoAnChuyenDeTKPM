using booking_api.Entities;
using booking_api.Hubs;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories.Bookings;
using booking_api.Models;
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
            var booking = new Booking();
            booking.Id = Guid.NewGuid();
            booking.UserId = info.UserId;
            booking.Name = info.Name;
            booking.Phone = info.Phone;
            booking.DiemDon = info.DiemDon;
            booking.DiemDen = info.DiemDen;
            booking.NgayTao = DateTime.Now;
            booking.Status = 1;
            var result = _bookingRepository.Insert(booking);
            if (result)
                await _broadcastHub.Clients.All.SendAsync("BroadcastBooking", JsonConvert.SerializeObject(booking));
            
            return Ok(new ResponseModel() { Success = result, Data = booking });
        }
        [HttpPost]
        public async Task<IActionResult> ReceiveBooking([FromBody] BookingInfo info)
        {
            var booking = await _bookingRepository.GetAsync(x => x.Id == info.Id);
            booking.Id = info.Id.Value;
            booking.DriverId = info.DriverId;           
            booking.Status = 2;
            var result = _bookingRepository.Update(booking);
            if(result)
                await _broadcastHub.Clients.Groups(booking.Id.ToString()).SendAsync("ReceiveBooking", JsonConvert.SerializeObject(booking));
            return Ok(new ResponseModel() { Success = result });
        }
    }
}
