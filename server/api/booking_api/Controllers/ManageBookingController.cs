using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ManageBookingController : ControllerBase
    {
        private readonly IUnitOfWork _uniOfWork;
        public ManageBookingController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ResponseModel() { Success = true, Data = await _uniOfWork.bookingRepository.GetAllAsync() });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHistoryBooking(Guid id)
        {
            var data = await _uniOfWork.bookingHistoryRepository.GetAllAsync(x => x.BookingId == id);
            return Ok(new ResponseModel() { Success = true, Data = data.OrderBy(x => x.Status) });
        }
    }
}
