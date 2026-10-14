using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Users.Commands.RestoreUser
{
    public sealed class RestoreUserCommandHnadler(IUnitOfWork unitOfWork) : IRequestHandler<RestoreUserCommand>
    {
        public async Task Handle(RestoreUserCommand request, CancellationToken cts)
        {
            var user = await unitOfWork.Repository<User>()
                                           .GetQueryable()
                                           .IgnoreQueryFilters()
                                           .FirstOrDefaultAsync(c => c.Email == request.dto.Email
                                                                  && c.Password == request.dto.Password, cts);

            if (user is null)
                throw new NotFoundException("User", request.dto.Email);

            user.IsDeleted = false;
            user.DeletedAt = null;

            var log = new AuditLog
            {
                EntityName = EntityName.User,
                EntityId = user.UserId,
                Action = ActionType.Restore,
                Reason = "Restore Account by the user",
                CreatedDate = DateTime.UtcNow,
                PerformedById = request.UserId,
                IsSelfAction = true,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent
            };

            await unitOfWork.Repository<AuditLog>().AddAsync(log, cts);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
