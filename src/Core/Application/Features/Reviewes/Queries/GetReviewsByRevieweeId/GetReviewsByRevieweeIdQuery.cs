using Core.Application.Features.Reviewes.Dto;
using MediatR;

namespace Core.Application.Features.Reviewes.Queries.GetReviewsByRevieweeId
{
    public sealed record GetReviewsByRevieweeIdQuery(int RevieweeId) : IRequest<IReadOnlyList<GetReviewsByRevieweeIdDto>>;
}
