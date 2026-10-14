using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;


namespace Core.Application.Features.Users.Commands.DeleteUser
{
    public sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cts)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if(user is null || user.Type != UserType.Customer)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var log = new AuditLog
            {
                EntityName = EntityName.User,
                EntityId = user.UserId,
                Action = ActionType.Delete,
                Reason = "Delete Account by the user",
                CreatedDate = DateTime.UtcNow,
                PerformedById = request.UserId,
                IsSelfAction = true,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent
            };

            unitOfWork.Repository<User>().Delete(user);
            await unitOfWork.Repository<AuditLog>().AddAsync(log, cts);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
