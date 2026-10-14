using FluentValidation;

namespace Core.Application.Features.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdQueryValidator : AbstractValidator<GetAddressByIdQuery>
    {
        public GetAddressByIdQueryValidator()
        {
            RuleFor(p => p.AddressId)
                .GreaterThan(0).WithMessage("Address Id must be greater than 0.");
        }
    }
}
