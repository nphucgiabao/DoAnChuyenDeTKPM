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
using booking_api.Features.Bookings.Mappers;

namespace booking_api.Features.Bookings.Queries
{
    public class GetBookingByIdQueryRequest : IRequest<Response<BookingModelResponse>>
    {
        public Guid? Id { get; set; }
        public class QueryValidation : AbstractValidator<GetBookingByIdQueryRequest>
        {
            public QueryValidation()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }
        public class QueryHandler : IRequestHandler<GetBookingByIdQueryRequest, Response<BookingModelResponse>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<QueryHandler> logger;
            public QueryHandler(IUnitOfWork unitOfWork, ILogger<QueryHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }

            public async Task<Response<BookingModelResponse>> Handle(GetBookingByIdQueryRequest request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var booking = await unitOfWork.bookingRepository.GetAsync(filter: x => x.Id == request.Id,
                        cancellationToken: cancellationToken);

                    return booking != null
                        ? new Response<BookingModelResponse>(booking.ToModel_Response())
                        : new Response<BookingModelResponse>(AppMessage.NotFoundData());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return new Response<BookingModelResponse>(AppMessage.Error(), ex);
                }
            }
        }
    }
}
