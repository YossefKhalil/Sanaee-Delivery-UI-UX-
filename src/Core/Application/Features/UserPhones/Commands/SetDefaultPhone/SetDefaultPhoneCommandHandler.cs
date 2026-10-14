
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.UserPhones.Commands.SetDefaultPhone
{
    public sealed class SetDefaultPhoneCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SetDefaultPhoneCommand>
    {
        public async Task Handle(SetDefaultPhoneCommand request, CancellationToken cts)
        {
            var NewPhone = await unitOfWork.Repository<UserPhone>().GetQueryable().FirstOrDefaultAsync(p => p.PhoneId == request.PhoneId && p.UserId == request.UserId, cts);

            if (NewPhone is null)
                throw new NotFoundException(nameof(UserPhone), request.PhoneId);

            var CurrentPhone = await unitOfWork.UserPhoneRepository.GetDefaultAsync(request.UserId, cts);

            if(CurrentPhone is not null && CurrentPhone.PhoneId != NewPhone.PhoneId)
            {
                CurrentPhone.IsDefault = false;
            }

            NewPhone.IsDefault = true;

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
