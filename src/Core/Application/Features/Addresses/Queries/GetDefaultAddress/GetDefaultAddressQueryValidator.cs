

using Core.Application.Features.Addresses.Queries.GetDefaultAddress;
using FluentValidation;

namespace Core.Application.Features.Addresses.Queries.GetUserAddresses
{
    public class GetDefaultAddressQueryValidator : AbstractValidator<GetUserAddressesQuery>
    {
        public GetDefaultAddressQueryValidator()
        {
            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("User Id must be greater than 0.");
        }
    }
}
