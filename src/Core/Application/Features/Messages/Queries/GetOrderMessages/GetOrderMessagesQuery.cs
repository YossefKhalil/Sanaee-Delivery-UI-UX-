using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Queries.GetOrderMessages
{
    public sealed record GetOrderMessagesQuery(int OrderId) : IRequest<IReadOnlyList<GetMessageDto>>;
}
