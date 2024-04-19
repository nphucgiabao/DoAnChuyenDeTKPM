using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
