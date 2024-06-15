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

namespace booking_api.Features.Drivers.Commands
{
    public class DeleteDriverCommandRequest : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public class CommandValidation : AbstractValidator<DeleteDriverCommandRequest>
        {
            public CommandValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class Handler : IRequestHandler<DeleteDriverCommandRequest, Response<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;
            public Handler(ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<bool>> Handle(DeleteDriverCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var driver = await _unitOfWork.driverRepository.GetAsync(filter: x => x.Id == request.Id);
                    var bookings = await _unitOfWork.bookingRepository.GetAllAsync(filter: x => x.DriverId == request.Id.ToString());
                    if (bookings.Count > 0)
                        return new Response<bool>("Tài xế đã thực hiện đơn hàng. Không thể xóa!");
                    if (driver == null)
                        return new Response<bool>("Not found!");
                    _unitOfWork.driverRepository.Delete(driver);
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
