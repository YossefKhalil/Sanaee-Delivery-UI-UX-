
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.UserPhones.Commands.DeleteUserPhone
{
    public sealed class DeleteUserPhoneCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserPhoneCommand>
    {
        public async Task Handle(DeleteUserPhoneCommand request, CancellationToken cts)
        {
            var phone = await unitOfWork.Repository<UserPhone>().GetByIdAsync(request.PhoneId, cts);

            //if (phone.IsDefault)
            //    throw new BadRequestException("This phone is default and can not deleted!");

            if (phone is null)
                throw new NotFoundException(nameof(UserPhone), request.PhoneId);

            unitOfWork.Repository<UserPhone>().Delete(phone);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
