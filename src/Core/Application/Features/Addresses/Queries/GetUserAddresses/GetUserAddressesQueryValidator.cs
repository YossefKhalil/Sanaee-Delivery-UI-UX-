

using Core.Application.Features.Addresses.Queries.GetDefaultAddress;
using FluentValidation;

namespace Core.Application.Features.Addresses.Queries.GetUserAddresses
{
    public class GetUserAddressesQueryValidator : AbstractValidator<GetUserAddressesQuery>
    {
        public GetUserAddressesQueryValidator()
        {
            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("User Id must be greater than 0.");
        }
    }
}
