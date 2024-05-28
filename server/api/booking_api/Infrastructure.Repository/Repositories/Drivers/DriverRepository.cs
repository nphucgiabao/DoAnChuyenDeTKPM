using booking_api.Infrastructure.Repository.Entities;

namespace booking_api.Infrastructure.Repository.Repositories.Drivers
{
    public class DriverRepository : RepositoryBase<car_bookingContext, Driver>, IDriverRepository
    {
        public DriverRepository(car_bookingContext context) : base(context) { }
    }
}
