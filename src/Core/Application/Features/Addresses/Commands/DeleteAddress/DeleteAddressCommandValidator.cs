

using FluentValidation;

namespace Core.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(p => p.AddressId)
                .GreaterThan(0).WithMessage("Address Id must be greater than 0.");
        }
    }
}
