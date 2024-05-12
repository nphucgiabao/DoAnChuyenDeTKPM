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
        private string data;
        public async Task BroadcastBooking()
        {
            //Action<string> action = async (value) =>
            //{
            //    await Clients.All.SendAsync("ReceiveBooking", "booking");
            //};
            //var data = string.Empty;
            RabbitMQSubcriber rabbitMQSubcriber = new RabbitMQSubcriber("booking", async (value) =>
            {
                await Clients.All.SendAsync("ReceiveBooking", value);
            });
            rabbitMQSubcriber.ProcessQueue();

            //await Clients.All.SendAsync("ReceiveInfo", "hello");
        }

    }
}
