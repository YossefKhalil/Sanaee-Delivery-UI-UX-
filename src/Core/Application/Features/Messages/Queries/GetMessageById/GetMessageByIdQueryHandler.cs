using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Messages.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Messages.Queries.GetMessageById
{
    public sealed class GetMessageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetMessageByIdQuery, GetMessageDto>
    {
        async Task<GetMessageDto> IRequestHandler<GetMessageByIdQuery, GetMessageDto>.Handle(GetMessageByIdQuery request, CancellationToken cts)
        {
            var message =
                await unitOfWork.Repository<Message>().GetByIdAsync(request.MessageId, cts);

            if (message is null)
                throw new NotFoundException(nameof(Message), request.MessageId);

            return mapper.Map<GetMessageDto>(message);
        }
    }
}
