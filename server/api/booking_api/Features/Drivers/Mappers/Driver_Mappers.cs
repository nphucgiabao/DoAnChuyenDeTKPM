using AutoMapper;
using booking_api.Features.Drivers.Models.Request;
using booking_api.Features.Drivers.Models.Responses;
using booking_api.Infrastructure.Repository.Entities;

namespace booking_api.Features.Drivers.Mappers
{
    public static class Driver_Mappers
    {
        internal static IMapper Mapper { get; }
        static Driver_Mappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                  .CreateMapper();
        }
        public static Driver ToModel_Request(this DriverModelRequest request)
        {
            if (request == null)
                return null;

            return Mapper.Map<Driver>(request);
        }
        public static DriverModelResponse ToModel_Response(this Driver request)
        {
            if (request == null)
                return null;

            return Mapper.Map<DriverModelResponse>(request);
        }
    }
}
