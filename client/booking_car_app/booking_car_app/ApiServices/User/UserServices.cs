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
        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.User> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Entities.User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> ResetPassword(string phone, string password)
        {
            return await PostRequest<Entities.User>("/api/User/ResetPassword", new Entities.User { PhoneNumber = phone, Password = password });
            //return response.Success;
        }

        public Task<bool> UpdateAsync(Entities.User entity)
        {
            throw new NotImplementedException();
        }
    }
}
