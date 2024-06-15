using booking_car_app.Models;
using System.Threading.Tasks;
using booking_car_app.Entities;
using System;

namespace booking_car_app.ApiServices.Drivers
{
    public interface IDriverService
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(Guid id);
        Task<ResponseModel> Delete(Guid id);
        Task<ResponseModel> AddEdit(Driver entity);
    }
}
