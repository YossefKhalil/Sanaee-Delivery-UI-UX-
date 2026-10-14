using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Messages.Commands.MarkAsRead
{
    public class MarkAsReadCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<MarkAsReadCommand>
    {
        public async Task Handle(MarkAsReadCommand request, CancellationToken cts)
        {
            var messages = await unitOfWork.Repository<Message>()
                                           .GetQueryable()
                                           .Where(m => m.ReadAt == null 
                                                    && m.SenderId == request.SenderId 
                                                    && m.OrderId == request.OrderId)
                                           .ToListAsync(cts);

            if (messages.Count == 0)
                throw new NotFoundException("There Are not any messages to read!");

            foreach (var m in messages)
                m.MarkAsRead();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
