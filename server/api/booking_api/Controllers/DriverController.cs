using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace booking_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DriverController : ControllerBase
    {
        private readonly IUnitOfWork _uniOfWork;
        public DriverController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ResponseModel() { Success = true, Data = await _uniOfWork.driverRepository.GetAllAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromBody]Driver info)
        {
            try
            {
                if (info.Id == Guid.Empty)
                {
                    info.Id = Guid.NewGuid();
                    _uniOfWork.driverRepository.Insert(info);
                }
                else
                    _uniOfWork.driverRepository.Update(info);
                var result = await _uniOfWork.Commit();
                if (result > 0)
                    return Ok(new ResponseModel() { Success = result > 0, Data = info, Message = "Save success" });
                return Ok(new ResponseModel() { Success = result > 0, Message = "Save error" });
            }
            catch(Exception ex)
            {
                return Ok(new ResponseModel() { Success = false, Message = ex.ToString() });
            }            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(new ResponseModel() { Success = true, Data = await _uniOfWork.driverRepository.GetAsync(x=>x.Id == id) });
        }
    }
}
