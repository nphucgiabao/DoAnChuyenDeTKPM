using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Models.RegisterViewModel, Entities.User>().ReverseMap();
        }
    }
}
