using Core.Application.Features.Messages.Dtos;
using MediatR;

namespace Core.Application.Features.Messages.Queries.GetMessageById
{
    public sealed record GetMessageByIdQuery(int MessageId) : IRequest<GetMessageDto>;
}
