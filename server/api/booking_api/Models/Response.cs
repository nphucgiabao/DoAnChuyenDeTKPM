using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Models
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public Response(string message, object error = null)
        {
            Success = false;
            Message = message;
            Error = error;
        }

        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public object Error { get; set; }
        public T Data { get; set; }
    }
}
