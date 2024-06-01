using booking_api.Features.Bookings.Mappers;
using booking_api.Features.Bookings.Models.Request;
using booking_api.Features.Bookings.Models.Responses;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Features.Bookings.Commands
{
    public class CreateBookingCommandRequest : BookingModelRequest, IRequest<Response<BookingModelResponse>>
    {

        public class Handler : IRequestHandler<CreateBookingCommandRequest, Response<BookingModelResponse>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;

            
            public Handler(ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;                
            }
            public async Task<Response<BookingModelResponse>> Handle(CreateBookingCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = request.ToModel_Request()!;
                    model.Id = Guid.NewGuid();                    
                    model.NgayTao = DateTime.Now;                    
                    _unitOfWork.bookingRepository.Insert(model);
                    _unitOfWork.bookingHistoryRepository.Insert(new BookingHistory()
                    {
                        Id = Guid.NewGuid(),
                        BookingId = model.Id,
                        Status = 1,
                        Time = DateTime.Now
                    });
                    var result = await _unitOfWork.Commit(cancellationToken);
                    return result > 0
                           ? new Response<BookingModelResponse>(model.ToModel_Response())
                           : new Response<BookingModelResponse>(AppMessage.Error());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new Response<BookingModelResponse>(AppMessage.Error(), ex.Message);
                }
            }
        }


    }
}
