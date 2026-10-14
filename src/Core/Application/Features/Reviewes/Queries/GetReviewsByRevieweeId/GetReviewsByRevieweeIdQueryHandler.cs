using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Reviewes.Dto;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Reviewes.Queries.GetReviewsByRevieweeId
{
    public sealed class GetReviewsByRevieweeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetReviewsByRevieweeIdQuery, IReadOnlyList<GetReviewsByRevieweeIdDto>>
    {
        public async Task<IReadOnlyList<GetReviewsByRevieweeIdDto>> Handle(GetReviewsByRevieweeIdQuery request, CancellationToken cts)
        {
            var reviewes = await unitOfWork.Repository<Review>()
                                           .GetQueryable()
                                           .AsNoTracking()
                                           .Where(r => r.RevieweeId == request.RevieweeId)
                                           .ProjectTo<GetReviewsByRevieweeIdDto>(mapper.ConfigurationProvider)
                                           .ToListAsync(cts);

            return reviewes;
        }
    }
}
