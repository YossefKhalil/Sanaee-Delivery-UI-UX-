using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Messages.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Messages.Queries.GetOrderMessages
{
    public sealed class GetOrderMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetOrderMessagesQuery, IReadOnlyList<GetMessageDto>>
    {

        async Task<IReadOnlyList<GetMessageDto>> IRequestHandler<GetOrderMessagesQuery, IReadOnlyList<GetMessageDto>>.Handle(GetOrderMessagesQuery request, CancellationToken cts)
        {
            var messages = await unitOfWork.Repository<Message>()
                                           .GetQueryable()
                                           .AsNoTracking()
                                           .Where(m => m.OrderId == request.OrderId)
                                           .ProjectTo<GetMessageDto>(mapper.ConfigurationProvider)
                                           .ToListAsync(cts);

            return messages;
        }
    }
}
