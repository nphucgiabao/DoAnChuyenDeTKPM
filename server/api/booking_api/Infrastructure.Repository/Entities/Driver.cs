using System;
using System.Collections.Generic;

namespace booking_api.Infrastructure.Repository.Entities
{
    public partial class Driver
    {
        public string Id { get; set; }
        public string BienSoXe { get; set; }
        public string Phone { get; set; }
        public string Avartar { get; set; }
        public int? TypeCar { get; set; }
    }
}
