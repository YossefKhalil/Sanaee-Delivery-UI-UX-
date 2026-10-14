

using FluentValidation;

namespace Core.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(p => p.AddressId)
                .GreaterThan(0).WithMessage("Address Id must be greater than 0.");

            RuleFor(a => a.address.Title)
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(a => a.address.BuildingNumber)
                .MaximumLength(20);

            RuleFor(a => a.address.ApartmentNumber)
                .MaximumLength(20);

            RuleFor(a => a.address.Notes)
                .MaximumLength(500);
        }
    }
}
