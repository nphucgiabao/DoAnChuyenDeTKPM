using booking_api.Features.Drivers.Models.Request;
using booking_api.Features.Drivers.Models.Responses;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using booking_api.Features.Drivers.Mappers;

namespace booking_api.Features.Drivers.Commands
{
    public class CreateDriverCommandRequest : DriverModelRequest, IRequest<Response<DriverModelResponse>>
    {
        public class Handler : IRequestHandler<CreateDriverCommandRequest, Response<DriverModelResponse>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;


            public Handler(ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<DriverModelResponse>> Handle(CreateDriverCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = request.ToModel_Request()!;
                    model.Id = Guid.NewGuid();                   
                    _unitOfWork.driverRepository.Insert(model);
                    
                    var result = await _unitOfWork.Commit(cancellationToken);
                    return result > 0
                           ? new Response<DriverModelResponse>(model.ToModel_Response(), AppMessage.Success("Lưu dữ liệu"))
                           : new Response<DriverModelResponse>(AppMessage.Error());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new Response<DriverModelResponse>(AppMessage.Error(), ex.Message);
                }
            }
        }
    }
}
