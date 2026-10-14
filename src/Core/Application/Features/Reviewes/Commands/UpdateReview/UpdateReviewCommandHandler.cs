using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Reviewes.Commands.UpdateReview
{
    public sealed class UpdateReviewCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateReviewCommand>
    {
        public async Task Handle(UpdateReviewCommand request, CancellationToken cts)
        {
            var review =
                await unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId, cts);

            if (review is null)
                throw new NotFoundException(nameof(Review), request.ReviewId);

            if (request.Rating is not null)
                review.Rating = request.Rating.Value;

            review.Comment = request.Comment;

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
