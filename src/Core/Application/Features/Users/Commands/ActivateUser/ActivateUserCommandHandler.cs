

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Users.Commands.ActivateUser
{
    public sealed class ActivateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ActivateUserCommand>
    {
        public async Task Handle(ActivateUserCommand request, CancellationToken cts)
        {
            var user = 
                await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if (user is null)
                throw new NotFoundException(nameof(User), request.UserId);

            user.Unfreeze();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
