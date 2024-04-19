using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices
{
    public interface IService<T, key> where T : class
    {
        Task<T> FindByIdAsync(key id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(key id);
    }
}
