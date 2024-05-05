using MessageQueue.Engines.RabbitMQ;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace driver_api.Hubs
{
    public class BroadcastHub : Hub
    {
        public void Broadcast()
        {
            RabbitMQSubcriber rabbitMQSubcriber = new RabbitMQSubcriber("booking", async (data) =>
            {
                await Clients.All.SendAsync("ReceiveBooking", data);
            });
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
