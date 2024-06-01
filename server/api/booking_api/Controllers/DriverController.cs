using booking_api.Features.Drivers.Commands;
using booking_api.Features.Drivers.Models.Responses;
using booking_api.Features.Drivers.Queries;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace booking_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<DriverModelResponse>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllDriverQueryRequest()));
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromBody]Driver info)
        {            
            if(info.Id == Guid.Empty)
            {
                var request = new CreateDriverCommandRequest();
                request.Name = info.Name;
                request.Phone = info.Phone;
                request.BienSoXe = info.BienSoXe;
                request.Avartar = info.Avartar;
                request.TypeCar = info.TypeCar;
                return Ok(await _mediator.Send(request));
            }
            else
            {
                var request = new UpdateDriverCommandRequest();
                request.Id = info.Id;
                request.Name = info.Name;
                request.Phone = info.Phone;
                request.BienSoXe = info.BienSoXe;
                request.Avartar = info.Avartar;
                request.TypeCar = info.TypeCar;
                return Ok(await _mediator.Send(request));
            }               
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<DriverModelResponse>>> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetDriverByIdQueryRequest() { Id = id }));
        }
    }
}
