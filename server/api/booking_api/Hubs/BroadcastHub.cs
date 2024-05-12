using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Hubs
{
    public class BroadcastHub : Hub
    {
        public async Task BroadcastBooking()
        {
            var cnId = Context.ConnectionId;
            await Clients.Client(cnId).SendAsync("ListBooking", "hello");
        }

        public async Task RefreshBroadcastBooking()
        {
            await Clients.All.SendAsync("ListBooking", "refresh");
        }
    }
}
