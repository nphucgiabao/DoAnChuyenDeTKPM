using booking_api.Features.Bookings.Models.Responses;
using booking_api.Features.Bookings.Queries;
using booking_api.Features.Drivers.Models.Responses;
using booking_api.Infrastructure.Repository.Repositories;
using booking_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using booking_api.Features.Drivers.Mappers;

namespace booking_api.Features.Drivers.Queries
{
    public class GetAllDriverQueryRequest : IRequest<Response<List<DriverModelResponse>>>
    {
        public class QueryValidation : AbstractValidator<GetAllBookingQueryRequest>
        {
            public QueryValidation()
            {

            }
        }
        public class QueryHandler : IRequestHandler<GetAllDriverQueryRequest, Response<List<DriverModelResponse>>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<QueryHandler> logger;
            public QueryHandler(IUnitOfWork unitOfWork, ILogger<QueryHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }

            public async Task<Response<List<DriverModelResponse>>> Handle(GetAllDriverQueryRequest request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var drivers = await unitOfWork.driverRepository.GetAllAsync(cancellationToken: cancellationToken);

                    return drivers.Count > 0
                        ? new Response<List<DriverModelResponse>>(drivers.Select(x => x.ToModel_Response()).ToList())
                        : new Response<List<DriverModelResponse>>(AppMessage.NotFoundData());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return new Response<List<DriverModelResponse>>(AppMessage.Error(), ex);
                }
            }
        }
    }
}
