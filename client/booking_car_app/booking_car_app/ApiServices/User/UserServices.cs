using booking_car_app.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.User
{
    public class UserServices : ServiceCore, IUserServices
    {
        public UserServices(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : 
            base(httpClientFactory, httpContextAccessor) {}
       
        public async Task<ResponseModel> Register(Entities.User entity)
        {
            return await PostRequest<Entities.User>("/user/register", entity);
        }

        public async Task<ResponseModel> ResetPassword(string phone, string password)
        {
            return await PostRequest<Entities.User>("/user/resetpassword", new Entities.User { PhoneNumber = phone, Password = password });
            //return response.Success;
        }
    }
}
