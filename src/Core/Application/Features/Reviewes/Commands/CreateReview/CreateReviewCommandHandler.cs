using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Reviewes.Dto;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Reviewes.Commands.CreateReview
{
    public class CreateReviewCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateReviewCommand, int>
    {
        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cts)
        {
            var review = new Review
            {
                Rating = request.dto.Rating,
                Comment = request.dto.Comment,
                RevieweeId = request.dto.RevieweeId,
                ReviewerId = request.ReviewerId,
                OrderId = request.dto.OrderId
            };

            await unitOfWork.Repository<Review>().AddAsync(review, cts);
            await unitOfWork.CompleteAsync(cts);

            return review.ReviewId;
        }
    }
}
