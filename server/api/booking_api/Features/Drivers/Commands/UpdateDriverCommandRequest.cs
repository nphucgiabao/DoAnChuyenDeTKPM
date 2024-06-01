using booking_api.Features.Drivers.Models.Request;
using booking_api.Infrastructure.Repository.Entities;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Features.Drivers.Commands
{
    public class UpdateDriverCommandRequest : DriverModelRequest, IRequest<Response<bool>>
    {
        public class CommandValidation : AbstractValidator<DriverModelRequest>
        {
            public CommandValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class CommandHandler : IRequestHandler<UpdateDriverCommandRequest, Response<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<CommandHandler> _logger;
            public CommandHandler(IUnitOfWork unitOfWork,
                ILogger<CommandHandler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
            public async Task<Response<bool>> Handle(UpdateDriverCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var driver = await _unitOfWork.driverRepository.GetAsync(filter: x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (driver == null)
                        return new Response<bool>(AppMessage.NotExistedById("Tài xế", request.Id.Value));

                    driver.Name = request.Name;
                    driver.Phone = request.Phone;
                    driver.BienSoXe = request.BienSoXe;
                    driver.TypeCar = request.TypeCar;
                    driver.Avartar = request.Avartar;                    

                    _unitOfWork.driverRepository.Update(driver);
                   
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
