using booking_api.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories.TypeCars
{
    public class TypeCarRepository : RepositoryBase<car_bookingContext, TypeCar>, ITypeCarRepository
    {
        public TypeCarRepository(car_bookingContext context) : base(context) { }
    }
}
