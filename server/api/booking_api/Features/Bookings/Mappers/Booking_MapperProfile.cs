using AutoMapper;
using booking_api.Features.Bookings.Models.Request;
using booking_api.Features.Bookings.Models.Responses;
using booking_api.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Features.Bookings.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingModelRequest, Booking>().ReverseMap();
            CreateMap<Booking, BookingModelResponse>().ReverseMap();
        }
    }
}
