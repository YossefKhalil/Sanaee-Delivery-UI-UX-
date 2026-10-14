

using FluentValidation;

namespace Core.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(a => a.City).NotEmpty().WithMessage("City is required.");

            RuleFor(a => a.Region).NotEmpty().WithMessage("Region is required.");

            RuleFor(a => a.Street).NotEmpty().WithMessage("Street is required.");

            RuleFor(a => a.BuildingNumber).MaximumLength(20);

            RuleFor(a => a.ApartmentNumber).MaximumLength(20);

            RuleFor(a => a.Notes).MaximumLength(500);

            RuleFor(a => a.UserId).GreaterThan(0).WithMessage("UserId is required");
        }
    }
}
