using booking_api.Entities;
using booking_api.Hubs;
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
        public BookingController(IHubContext<BroadcastHub> broadcastHub)
        {
            _broadcastHub = broadcastHub;
        }        
        [HttpPost]
        public async Task<IActionResult> FindDriver([FromBody] BookingInfo info)
        {
            //RabbitMQPublisher<BookingInfo> rabbitMQPublisher = new RabbitMQPublisher<BookingInfo>("booking");
            //rabbitMQPublisher.SendMessage(info).Start();
            var data = JsonConvert.SerializeObject(info);
            await _broadcastHub.Clients.All.SendAsync("BroadcastBooking", data);
            
            return Ok();
        }
    }
}
