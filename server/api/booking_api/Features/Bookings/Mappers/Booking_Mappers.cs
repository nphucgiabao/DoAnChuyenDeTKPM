using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using booking_api.Features.Bookings.Models.Request;
using booking_api.Features.Bookings.Models.Responses;
using booking_api.Infrastructure.Repository.Entities;

namespace booking_api.Features.Bookings.Mappers
{
    public static class Booking_Mappers
    {
        internal static IMapper Mapper { get; }
        static Booking_Mappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                  .CreateMapper();
        }
        public static Booking ToModel_Request(this BookingModelRequest request)
        {
            if (request == null)
                return null;

            return Mapper.Map<Booking>(request);
        }
        public static BookingModelResponse ToModel_Response(this Booking request)
        {
            if (request == null)
                return null;

            return Mapper.Map<BookingModelResponse>(request);
        }
    }
}
