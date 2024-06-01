using booking_api.Features.Bookings.Models.Responses;
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
using booking_api.Features.Bookings.Mappers;

namespace booking_api.Features.Bookings.Queries
{
    public class GetBookingByDriverIdQueryRequest : IRequest<Response<List<BookingModelResponse>>>
    {
        public string DriverId { get; set; }
        public class QueryValidation : AbstractValidator<GetBookingByDriverIdQueryRequest>
        {
            public QueryValidation()
            {
                RuleFor(x => x.DriverId).NotNull().NotEmpty();
            }
        }
        public class QueryHandler : IRequestHandler<GetBookingByDriverIdQueryRequest, Response<List<BookingModelResponse>>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<QueryHandler> logger;
            public QueryHandler(IUnitOfWork unitOfWork, ILogger<QueryHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }

            public async Task<Response<List<BookingModelResponse>>> Handle(GetBookingByDriverIdQueryRequest request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var bookings = await unitOfWork.bookingRepository.GetAllAsync(filter: x => x.DriverId == request.DriverId,
                        cancellationToken: cancellationToken);

                    return bookings.Count > 0
                        ? new Response<List<BookingModelResponse>>(bookings.Select(x => x.ToModel_Response()).OrderBy(x => x!.NgayTao).ToList())
                        : new Response<List<BookingModelResponse>>(AppMessage.NotFoundData());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return new Response<List<BookingModelResponse>>(AppMessage.Error(), ex);
                }
            }
        }
    }
}
