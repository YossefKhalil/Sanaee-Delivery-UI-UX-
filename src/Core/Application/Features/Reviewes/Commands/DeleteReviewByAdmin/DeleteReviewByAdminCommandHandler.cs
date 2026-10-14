using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Reviewes.Commands.DeleteReviewByAdmin
{
    public sealed class DeleteReviewByAdminCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteReviewByAdminCommand>
    {
        public async Task Handle(DeleteReviewByAdminCommand request, CancellationToken cts)
        {
            var review =
                await unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId, cts);

            if (review is null)
                throw new NotFoundException(nameof(Review), request.ReviewId);

            unitOfWork.Repository<Review>().Delete(review);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
