using booking_car_app.Models;
using System.Threading.Tasks;
using booking_car_app.Entities;

namespace booking_car_app.ApiServices.Drivers
{
    public interface IDriverService
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> AddEdit(Driver entity);
    }
}
