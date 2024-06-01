using booking_api.Infrastructure.Repository.Entities;
using AutoMapper;
using booking_api.Features.Drivers.Models.Request;
using booking_api.Features.Drivers.Models.Responses;

namespace booking_api.Features.Drivers.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DriverModelRequest, Driver>().ReverseMap();
            CreateMap<Driver, DriverModelResponse>().ReverseMap();
        }
    }
}
