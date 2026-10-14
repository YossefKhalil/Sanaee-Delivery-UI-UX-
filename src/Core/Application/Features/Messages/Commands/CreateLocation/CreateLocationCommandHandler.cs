using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Features.Messages.Commands.CreateLocation
{
    public sealed class CreateLocationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateLocationCommand, int>
    {
        public async Task<int> Handle(CreateLocationCommand request, CancellationToken cts)
        {
            var locationMessage = Message.CreateLocation
                (
                    request.SenderId,
                    request.dto.RecipientId,
                    request.dto.OrderId,
                    request.dto.Latitude,
                    request.dto.Longitude
                );

            await unitOfWork.Repository<Message>().AddAsync(locationMessage, cts);
            await unitOfWork.CompleteAsync(cts);

            return locationMessage.Id;
        }
    }
}
