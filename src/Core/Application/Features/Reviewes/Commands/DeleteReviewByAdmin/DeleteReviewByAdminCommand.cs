using MediatR;

namespace Core.Application.Features.Reviewes.Commands.DeleteReviewByAdmin
{
    public sealed record DeleteReviewByAdminCommand(int ReviewId) : IRequest;
}
