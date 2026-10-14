using Core.Application.Features.Users.Commands.DeActivateUser;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.DeActivateUser
{
    public sealed class DeActivateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeActivateUserCommand>
    {
        public async Task Handle(DeActivateUserCommand request, CancellationToken cts)
        {
            var user =
                await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if (user is null)
                throw new NotFoundException(nameof(User), request.UserId);

            user.Freeze();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
