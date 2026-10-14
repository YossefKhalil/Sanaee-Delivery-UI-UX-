using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Commands.CreateLocation
{
    public sealed record CreateLocationCommand
        (
            int SenderId,
            CreateLocationDto dto

        ) : IRequest<int>;

}
