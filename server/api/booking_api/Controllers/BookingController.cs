using booking_api.Entities;
using booking_api.Hubs;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
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
        private readonly IUnitOfWork _uniOfWork;        
        public BookingController(IHubContext<BroadcastHub> broadcastHub, IUnitOfWork unitOfWork)
        {
            _broadcastHub = broadcastHub;
            _uniOfWork = unitOfWork;
           
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
            booking.UnitPrice = info.UnitPrice;
            booking.Status = 1;
            _uniOfWork.bookingRepository.Insert(booking);
            var result = await _uniOfWork.Commit();
            if (result > 0)
                await _broadcastHub.Clients.All.SendAsync("BroadcastBooking", JsonConvert.SerializeObject(booking));
            
            return Ok(new ResponseModel() { Success = result > 0, Data = booking });
        }
        [HttpPost]
        public async Task<IActionResult> ReceiveBooking([FromBody] BookingInfo info)
        {
            var booking = await _uniOfWork.bookingRepository.GetAsync(x => x.Id == info.Id);
            booking.Id = info.Id.Value;
            booking.DriverId = info.DriverId;           
            booking.Status = 2;
            _uniOfWork.bookingRepository.Update(booking);
            var result = await _uniOfWork.Commit();
            if(result > 0)
            {
                var driver = await _uniOfWork.driverRepository.GetAsync(x => x.Id == Guid.Parse(info.DriverId));
                await _broadcastHub.Clients.Groups(booking.Id.ToString()).SendAsync("ReceiveBooking", driver);
            }                
            return Ok(new ResponseModel() { Success = result > 0 });
        }

        [HttpGet]
        [Route("{distance}/{idType}")]
        public async Task<IActionResult> UnitPrice(decimal distance, int idType)
        {
            var type = await _uniOfWork.typeCarRepository.GetAsync(x => x.Id == idType);
            if (distance <= 2)
                return Ok(new ResponseModel() { Data = new { Price = distance * type.GiaCuoc2Kmdau } });
            else
                return Ok(new ResponseModel() { Data = new { Price = 2 * type.GiaCuoc2Kmdau + ((distance - 2) * type.GiaCuocSau2Km) } });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var booking = await _uniOfWork.bookingRepository.GetAsync(x => x.Id == id);
            return Ok(new ResponseModel() { Data = booking });
        }
    }
}
