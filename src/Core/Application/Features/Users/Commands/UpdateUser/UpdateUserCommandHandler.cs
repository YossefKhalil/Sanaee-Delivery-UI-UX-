using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cts)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if(user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (!string.IsNullOrWhiteSpace(request.User.FirstName))
                user.FirstName = request.User.FirstName;

            if (!string.IsNullOrWhiteSpace(request.User.LastName))
                user.LastName = request.User.LastName;

            if (!string.IsNullOrWhiteSpace(request.User.Email))
                user.Email = request.User.Email;


            user.UpdatedAt = DateTime.UtcNow;

            unitOfWork.Repository<User>().Update(user);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
