using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Auth.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cts)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if(user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            user.Password = request.NewPassword;

            unitOfWork.Repository<User>().Update(user);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
