

using FluentValidation;

namespace Core.Application.Features.Addresses.Commands.SetDefaultAddress
{
    public class SetDefaultAddressCommandValidator : AbstractValidator<SetDefaultAddressCommand>
    {
        public SetDefaultAddressCommandValidator()
        {
            RuleFor(p => p.AddressId)
                .GreaterThan(0).WithMessage("Address Id must be greater than 0.");


            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("User Id must be greater than 0.");
        }
    }
}
