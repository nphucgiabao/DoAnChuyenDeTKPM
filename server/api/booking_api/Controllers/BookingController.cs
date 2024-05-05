using booking_api.Entities;
using MessageQueue.Engines.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public async Task<IActionResult> FindDriver([FromBody] BookingInfo info)
        {
            RabbitMQPublisher<BookingInfo> rabbitMQPublisher = new RabbitMQPublisher<BookingInfo>("booking");
            await rabbitMQPublisher.SendMessage(info);
            return Ok();
        }
    }
}
