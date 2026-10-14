using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateMedia
{
    public sealed class CreateMediaCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMediaCommand, int>
    {
        public async Task<int> Handle(CreateMediaCommand request, CancellationToken cts)
        {
            var MediaMessage = Message.CreateMedia
                (
                    request.SenderId,
                    request.dto.RecipientId,
                    request.dto.OrderId,
                    request.dto.Type,
                    request.dto.AttachmentUrl,
                    request.dto.Content
                );

            await unitOfWork.Repository<Message>().AddAsync(MediaMessage, cts);
            await unitOfWork.CompleteAsync(cts);

            return MediaMessage.Id;
        }
    }
}
