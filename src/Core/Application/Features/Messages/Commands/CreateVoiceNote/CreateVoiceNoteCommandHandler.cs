using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateVoiceNote
{
    public sealed class CreateVoiceNoteCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateVoiceNoteCommand, int>
    {
        public async Task<int> Handle(CreateVoiceNoteCommand request, CancellationToken cts)
        {
            var voiceNoteMessage = Message.CreateVoiceNote
                (
                    request.SenderId,
                    request.dto.RecipientId,
                    request.dto.OrderId,
                    request.dto.AttachmentUrl,
                    request.dto.DurationInSeconds
                );

            await unitOfWork.Repository<Message>().AddAsync(voiceNoteMessage, cts);
            await unitOfWork.CompleteAsync(cts);

            return voiceNoteMessage.Id;
        }
    }
}
