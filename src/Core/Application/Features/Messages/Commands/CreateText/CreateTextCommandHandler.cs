using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateText
{
    public sealed class CreateTextCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTextCommand, int>
    {
        public async Task<int> Handle(CreateTextCommand request, CancellationToken cts)
        {
            var MediaMessage = Message.CreateText
                (
                    request.SenderId,
                    request.dto.RecipientId,
                    request.dto.OrderId,
                    request.dto.Content
                );

            await unitOfWork.Repository<Message>().AddAsync(MediaMessage, cts);
            await unitOfWork.CompleteAsync(cts);

            return MediaMessage.Id;
        }
    }
}
