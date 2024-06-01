using booking_api.Features.Bookings.Models.Responses;
using booking_api.Features.Bookings.Queries;
using booking_api.Features.Drivers.Models.Responses;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using booking_api.Features.Drivers.Mappers;

namespace booking_api.Features.Drivers.Queries
{
    public class GetDriverByIdQueryRequest : IRequest<Response<DriverModelResponse>>
    {
        public Guid? Id { get; set; }
        public class QueryValidation : AbstractValidator<GetDriverByIdQueryRequest>
        {
            public QueryValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class QueryHandler : IRequestHandler<GetDriverByIdQueryRequest, Response<DriverModelResponse>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<QueryHandler> logger;
            public QueryHandler(IUnitOfWork unitOfWork, ILogger<QueryHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }

            public async Task<Response<DriverModelResponse>> Handle(GetDriverByIdQueryRequest request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var driver = await unitOfWork.driverRepository.GetAsync(filter: x => x.Id == request.Id,
                        cancellationToken: cancellationToken);

                    return driver != null
                        ? new Response<DriverModelResponse>(driver.ToModel_Response())
                        : new Response<DriverModelResponse>(AppMessage.NotFoundData());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return new Response<DriverModelResponse>(AppMessage.Error(), ex);
                }
            }
        }
    }
}
