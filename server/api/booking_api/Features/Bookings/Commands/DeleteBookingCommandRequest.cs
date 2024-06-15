using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Features.Bookings.Commands
{
    public class DeleteBookingCommandRequest : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public class CommandValidation : AbstractValidator<DeleteBookingCommandRequest>
        {
            public CommandValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class Handler : IRequestHandler<DeleteBookingCommandRequest, Response<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;
            public Handler(ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<bool>> Handle(DeleteBookingCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var booking = await _unitOfWork.bookingRepository.GetAsync(filter: x => x.Id == request.Id);
                    if (booking == null)
                        return new Response<bool>("Not found!");
                    _unitOfWork.bookingRepository.Delete(booking);
                    var result = await _unitOfWork.Commit(cancellationToken);
                    return result > 0
                           ? new Response<bool>(true)
                           : new Response<bool>(AppMessage.Error());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new Response<bool>(AppMessage.Error(), ex.Message);
                }
            }
        }
    }
}
