using booking_api.Infrastructure.Repository.Repositories.Bookings;
using booking_api.Infrastructure.Repository.Repositories.TypeCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly car_bookingContext context;
        private IBookingRepository _bookingRepository;
        private ITypeCarRepository _typeCarRepository;
        public UnitOfWork(car_bookingContext context)
        {
            this.context = context;
        }
        public IBookingRepository bookingRepository => _bookingRepository ?? (_bookingRepository = new BookingRepository(this.context));

        public ITypeCarRepository typeCarRepository => _typeCarRepository ?? (_typeCarRepository = new TypeCarRepository(this.context));

        public Task<int> Commit(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
