using booking_api.Infrastructure.Repository.Repositories.Bookings;
using booking_api.Infrastructure.Repository.Repositories.TypeCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IBookingRepository bookingRepository { get; }
        public ITypeCarRepository typeCarRepository { get; }
        public Task<int> Commit(CancellationToken cancellationToken = default);
    }
}
