using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Models
{
    public class ResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
