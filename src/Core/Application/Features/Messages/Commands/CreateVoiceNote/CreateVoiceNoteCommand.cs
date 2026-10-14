using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateVoiceNote
{
    public sealed record CreateVoiceNoteCommand
        (
            int SenderId,
            CreateVoiceNoteDto dto

        ) : IRequest<int>;
}
