using booking_car_app.Entities;
using booking_car_app.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices.Drivers
{
    public class DriverServices : ServiceCore, IDriverService
    {
        public DriverServices(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<ResponseModel> AddEdit(Driver entity)
        {
            return await PostRequest<Driver>("/driver/addEdit", entity);
        }

        public async Task<ResponseModel> Delete(Guid id)
        {
            return await DeleteRequest($"/driver/Delete/{id}");
        }

        public async Task<ResponseModel> GetAll()
        {
            return await GetRequest("/driver/getAll");
        }

        public async Task<ResponseModel> GetById(Guid id)
        {
            return await GetRequest($"/driver/getById/{id}");
        }
    }
}
