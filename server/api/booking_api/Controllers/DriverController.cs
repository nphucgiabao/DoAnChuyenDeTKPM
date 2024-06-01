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
        //private readonly IUnitOfWork _uniOfWork;
        private readonly IMediator _mediator;
        public DriverController(IMediator mediator)
        {
            //_uniOfWork = unitOfWork;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<DriverModelResponse>>>> GetAll()
        {
            //return Ok(new ResponseModel() { Success = true, Data = await _uniOfWork.driverRepository.GetAllAsync() });
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
            //try
            //{
            //    if (info.Id == Guid.Empty)
            //    {
            //        info.Id = Guid.NewGuid();
            //        _uniOfWork.driverRepository.Insert(info);
            //    }
            //    else
            //        _uniOfWork.driverRepository.Update(info);
            //    var result = await _uniOfWork.Commit();
            //    if (result > 0)
            //        return Ok(new ResponseModel() { Success = result > 0, Data = info, Message = "Save success" });
            //    return Ok(new ResponseModel() { Success = result > 0, Message = "Save error" });
            //}
            //catch(Exception ex)
            //{
            //    return Ok(new ResponseModel() { Success = false, Message = ex.ToString() });
            //}            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<DriverModelResponse>>> GetById(Guid id)
        {
            //return Ok(new ResponseModel() { Success = true, Data = await _uniOfWork.driverRepository.GetAsync(x=>x.Id == id) });
            return Ok(await _mediator.Send(new GetDriverByIdQueryRequest() { Id = id }));
        }
    }
}
