using Core.Application.Features.Reviewes.Dto;
using MediatR;

namespace Core.Application.Features.Reviewes.Commands.CreateReview
{
    public sealed record CreateReviewCommand(int ReviewerId, CreateReviewDto dto) : IRequest<int>;
}
