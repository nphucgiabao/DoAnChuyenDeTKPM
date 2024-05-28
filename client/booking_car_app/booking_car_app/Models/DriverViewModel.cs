using System;

namespace booking_car_app.Models
{
    public class DriverViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BienSoXe { get; set; }
        public string Phone { get; set; }
        public string Avartar { get; set; }
        public int TypeCar { get; set; }
    }
}
