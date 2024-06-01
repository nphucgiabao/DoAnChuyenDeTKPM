using booking_api.Features.Bookings.Models.Request;
using booking_api.Features.Bookings.Models.Responses;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using booking_api.Infrastructure.Repository.Entities;

namespace booking_api.Features.Bookings.Commands
{
    public class UpdateBookingCommandRequest : BookingModelRequest, IRequest<Response<bool>>
    {
        public class CommandValidation : AbstractValidator<UpdateBookingCommandRequest>
        {
            public CommandValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class CommandHandler : IRequestHandler<UpdateBookingCommandRequest, Response<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<CommandHandler> _logger;
            public CommandHandler(IUnitOfWork unitOfWork,
                ILogger<CommandHandler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Response<bool>> Handle(UpdateBookingCommandRequest request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var booking = await _unitOfWork.bookingRepository.GetAsync(filter: x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (booking == null)
                        return new Response<bool>(AppMessage.NotExistedById("Đơn hàng", request.Id.Value));

                    booking.DriverId = request.DriverId;
                    booking.Status = request.Status;                    

                    _unitOfWork.bookingRepository.Update(booking);
                    _unitOfWork.bookingHistoryRepository.Insert(new BookingHistory()
                    {
                        Id = Guid.NewGuid(),
                        BookingId = request.Id.Value,
                        Status = request.Status,
                        Time = DateTime.Now
                    });
                    var result = await _unitOfWork.Commit(cancellationToken);

                    return result > 0
                        ? new Response<bool>(true)
                        : new Response<bool>(AppMessage.Error());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new Response<bool>(AppMessage.Error(), ex);
                }
            }
        }
    }
}
