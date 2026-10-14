using Application.Features.Reviewes.Dto;
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Core.Application.Features.Reviewes.Queries.GetReviewById
{
    public sealed class GetReviewByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetReviewByIdQuery, GetReviewByIdDto>
    {
        public async Task<GetReviewByIdDto> Handle(GetReviewByIdQuery request, CancellationToken cts)
        {
            var review = 
                await unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId, cts);


            if (review is null)
                throw new NotFoundException(nameof(Review), request.ReviewId);


            return mapper.Map<GetReviewByIdDto>(review);
        }
    }
}
