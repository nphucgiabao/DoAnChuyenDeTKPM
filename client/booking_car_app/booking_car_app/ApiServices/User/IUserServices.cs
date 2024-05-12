using booking_car_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.User
{
    public interface IUserServices
    {
        Task<ResponseModel> ResetPassword(string phone, string password);
        Task<ResponseModel> Register(Entities.User entity);
        Task<ResponseModel> GetUsers();
    }
}
