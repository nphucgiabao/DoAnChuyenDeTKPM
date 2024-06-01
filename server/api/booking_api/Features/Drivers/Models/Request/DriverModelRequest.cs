using System;

namespace booking_api.Features.Drivers.Models.Request
{
    public class DriverModelRequest
    {
        public Guid? Id { get; set; }
        public string BienSoXe { get; set; }
        public string Phone { get; set; }
        public string Avartar { get; set; }
        public int? TypeCar { get; set; }
        public string Name { get; set; }
    }
}
