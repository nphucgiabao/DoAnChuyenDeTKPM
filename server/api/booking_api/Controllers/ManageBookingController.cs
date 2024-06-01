using booking_api.Features.Bookings.Models.Responses;
using booking_api.Features.Bookings.Queries;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IMediator _mediator;
        public ManageBookingController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _uniOfWork = unitOfWork;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<BookingModelResponse>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBookingQueryRequest()));
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
