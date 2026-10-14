
using Core.Application.Features.ApplicationHistories.Dtos;
using MediatR;

namespace Core.Application.Features.ApplicationHistories.Commands.CreateApplicationHistory
{
    public sealed record CreateApplicationHistoryCommand
    (

        ApplicationHistoryDto dto,
        int ChangedBy

    ) : IRequest<int>;
}
