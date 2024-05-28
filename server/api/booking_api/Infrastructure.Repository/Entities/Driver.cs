using System;
using System.Collections.Generic;

namespace booking_api.Infrastructure.Repository.Entities
{
    public partial class Driver
    {
        public Guid Id { get; set; }
        public string BienSoXe { get; set; }
        public string Phone { get; set; }
        public string Avartar { get; set; }
        public int? TypeCar { get; set; }
        public string Name { get; set; }
    }
}
