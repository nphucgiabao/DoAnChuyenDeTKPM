using booking_car_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.User
{
    public interface IUserServices : IService<Entities.User, string>
    {
        Task<ResponseModel> ResetPassword(string phone, string password);
    }
}
