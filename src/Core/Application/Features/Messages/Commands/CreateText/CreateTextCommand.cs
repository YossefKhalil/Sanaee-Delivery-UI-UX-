using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateText
{
    public sealed record CreateTextCommand
        (
            int SenderId,
            CreateTextDto dto

        ) : IRequest<int>;
}
