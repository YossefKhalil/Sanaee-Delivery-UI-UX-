using Core.Application.Features.Addresses.Dtos;
using MediatR;


namespace Core.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand(

        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        string OtpCode,
        CreateAddressByUserEntity? Address = null
        
    ) : IRequest<int>;
    
}
