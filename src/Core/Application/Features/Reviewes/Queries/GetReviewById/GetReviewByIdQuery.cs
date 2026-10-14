using Application.Features.Reviewes.Dto;
using MediatR;

namespace Core.Application.Features.Reviewes.Queries.GetReviewById
{
    public sealed record GetReviewByIdQuery(int ReviewId) : IRequest<GetReviewByIdDto>;
}
