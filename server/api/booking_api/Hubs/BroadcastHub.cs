using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories.Bookings;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Hubs
{
    public class BroadcastHub : Hub
    {
        private readonly IBookingRepository _bookingRepository;
        public BroadcastHub(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task BroadcastBooking()
        {
            var cnId = Context.ConnectionId;
            var listBooking = await _bookingRepository.GetAllAsync(x => x.Status == 1);
            await Clients.Client(cnId).SendAsync("ListBooking", 
                JsonConvert.SerializeObject(listBooking.Select(x=>new { x.Id, x.DiemDen, x.DiemDon, x.Name, x.Phone })));
        }

        public async Task RefreshBroadcastBooking()
        {
            await Clients.All.SendAsync("ListBooking", "refresh");
        }
    }
}
