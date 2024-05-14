using System;
using System.Collections.Generic;

namespace booking_api.Infrastructure.Repository.Entities
{
    public partial class TypeCar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? GiaCuoc2Kmdau { get; set; }
        public decimal? GiaCuocSau2Km { get; set; }
    }
}
