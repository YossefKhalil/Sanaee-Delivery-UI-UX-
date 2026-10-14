using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateMedia
{
    public sealed record CreateMediaCommand
        (
            int SenderId,
            CreateMediaDto dto

        ) : IRequest<int>;
}
