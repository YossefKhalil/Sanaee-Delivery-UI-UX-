using MediatR;

namespace Core.Application.Features.Reviewes.Commands.UpdateReview
{
    public sealed record UpdateReviewCommand(int ReviewId, int? Rating, string? Comment) : IRequest;
}
