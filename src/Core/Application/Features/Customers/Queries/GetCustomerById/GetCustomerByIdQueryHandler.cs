

using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Customers.Dtos;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.Customers.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetCustomerByIdQuery request, CancellationToken cts)
        {
            var customer = await unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cts);

            if (customer == null || customer.Type != UserType.Customer)
                throw new NotFoundException(nameof(User), request.UserId);

            return mapper.Map<UserDto>(customer);
        }
    }
}
