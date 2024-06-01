using booking_api.Entities;
using booking_api.Features.Bookings.Commands;
using booking_api.Features.Bookings.Models.Responses;
using booking_api.Features.Bookings.Queries;
using booking_api.Features.Drivers.Queries;
using booking_api.Hubs;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> _broadcastHub;
        private readonly IUnitOfWork _uniOfWork;
        private readonly IMediator _mediator;
        public BookingController(IHubContext<BroadcastHub> broadcastHub, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _broadcastHub = broadcastHub;
            _uniOfWork = unitOfWork;
            _mediator = mediator;
        }        
        [HttpPost]
        public async Task<ActionResult<Response<BookingModelResponse>>> FindDriver([FromBody] BookingInfo info)
        {                   
            var request = new CreateBookingCommandRequest();            
            request.UserId = info.UserId;
            request.Name = info.Name;
            request.Phone = info.Phone;
            request.DiemDon = info.DiemDon;
            request.DiemDen = info.DiemDen;            
            request.UnitPrice = info.UnitPrice.Value;
            request.Status = 1;         
            var result = await _mediator.Send(request);
            if(result.Success)
                await _broadcastHub.Clients.All.SendAsync("BroadcastBooking", JsonConvert.SerializeObject(result.Data));
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> ReceiveBooking([FromBody] BookingInfo info)
        //{
        //    try
        //    {
        //        var booking = await _uniOfWork.bookingRepository.GetAsync(x => x.Id == info.Id);
        //        //booking.Id = info.Id.Value;
        //        booking.DriverId = info.DriverId;
        //        booking.Status = 2;
        //        _uniOfWork.bookingRepository.Update(booking);
        //        _uniOfWork.bookingHistoryRepository.Insert(new BookingHistory()
        //        {
        //            Id = Guid.NewGuid(),
        //            BookingId = booking.Id,
        //            Status = 2,
        //            Time = DateTime.Now
        //        });
        //        var result = await _uniOfWork.Commit();
        //        if (result > 0)
        //        {
        //            var driver = await _uniOfWork.driverRepository.GetAsync(x => x.Id == Guid.Parse(info.DriverId));
        //            await _broadcastHub.Clients.Groups(booking.Id.ToString()).SendAsync("ReceiveBooking", driver);
        //        }
        //        return Ok(new ResponseModel() { Success = result > 0 });
        //    }
        //    catch(Exception ex)
        //    {
        //        return Ok(new ResponseModel() { Success = false, Message = ex.ToString() });
        //    }
           
        //}

        [HttpPost]
        public async Task<IActionResult> UpdateStatusBooking([FromBody] BookingInfo info)
        {
            var request = new UpdateBookingCommandRequest();
            request.Id = info.Id;
            request.DriverId = info.DriverId;
            request.Status = info.Status;
            var result = await _mediator.Send(request);
            if (result.Data)
            {
                if(info.Status == 2)
                {
                    var requestDriver = new GetDriverByIdQueryRequest() { Id = Guid.Parse(info.DriverId) };
                    var driver = await _mediator.Send(requestDriver);
                    await _broadcastHub.Clients.Groups(info.Id.ToString()).SendAsync("ReceiveBooking", driver.Data);
                }
                else
                    await _broadcastHub.Clients.Groups(info.Id.ToString()).SendAsync("UpdateStatusBooking", info.Status);
            }
                
            return Ok(result);
            //try
            //{
            //    var booking = await _uniOfWork.bookingRepository.GetAsync(x => x.Id == info.Id);
            //    //booking.Id = info.Id.Value;
            //    //booking.DriverId = info.DriverId;
            //    booking.Status = info.Status;
            //    _uniOfWork.bookingRepository.Update(booking);
            //    _uniOfWork.bookingHistoryRepository.Insert(new BookingHistory()
            //    {
            //        Id = Guid.NewGuid(),
            //        BookingId = booking.Id,
            //        Status = info.Status,
            //        Time = DateTime.Now
            //    });
            //    var result = await _uniOfWork.Commit();
            //    if (result > 0)
            //    {         
            //        await _broadcastHub.Clients.Groups(booking.Id.ToString()).SendAsync("UpdateStatusBooking", info.Status);           
            //    }
            //    return Ok(new ResponseModel() { Success = result > 0 });
            //}
            //catch (Exception ex)
            //{
            //    return Ok(new ResponseModel() { Success = false, Message = ex.ToString() });
            //}

        }

        [HttpGet]
        [Route("{distance}/{idType}")]
        public async Task<IActionResult> UnitPrice(decimal distance, int idType)
        {
            var type = await _uniOfWork.typeCarRepository.GetAsync(x => x.Id == idType);
            if (distance <= 2)
                return Ok(new ResponseModel() { Data = new { Price = distance * type.GiaCuoc2Kmdau } });
            else
                return Ok(new ResponseModel() { Data = new { Price = 2 * type.GiaCuoc2Kmdau + ((distance - 2) * type.GiaCuocSau2Km) } });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<BookingModelResponse>>> GetBookingById(Guid id)
        {
            //var booking = await _uniOfWork.bookingRepository.GetAsync(x => x.Id == id);
            return Ok(await _mediator.Send(new GetBookingByIdQueryRequest() { Id = id }));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<List<BookingModelResponse>>>> GetBookingByUserId(string id)
        {
            //var booking = await _uniOfWork.bookingRepository.GetAllAsync(x => x.UserId == id);
            return Ok(await _mediator.Send(new GetBookingByUserIdQueryRequest() { UserId = id}));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<List<BookingModelResponse>>>> GetBookingByDriverId(string id)
        {
            //var booking = await _uniOfWork.bookingRepository.GetAllAsync(x => x.DriverId == id);
            return Ok(await _mediator.Send(new GetBookingByDriverIdQueryRequest() { DriverId = id }));
        }
    }
}
