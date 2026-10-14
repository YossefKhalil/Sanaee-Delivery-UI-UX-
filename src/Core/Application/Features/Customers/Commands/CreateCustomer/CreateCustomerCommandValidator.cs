using FluentValidation;


namespace Core.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First Name is required!")
                .MaximumLength(50).WithMessage("First Name must not exceed 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is required!")
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required!")
                .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required!");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required!");

            RuleFor(u => u.OtpCode)
                .NotEmpty().WithMessage("Otp Code is required!");


            When(u => u.Address is not null, () =>
            {

                RuleFor(a => a.Address!.Title)
                    .NotEmpty().WithMessage("Title is required.")
                    .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

                RuleFor(a => a.Address!.City).NotEmpty().WithMessage("City is required.");

                RuleFor(a => a.Address!.Region).NotEmpty().WithMessage("Region is required.");

                RuleFor(a => a.Address!.Street).NotEmpty().WithMessage("Street is required.");

                RuleFor(a => a.Address!.BuildingNumber).MaximumLength(20);

                RuleFor(a => a.Address!.ApartmentNumber).MaximumLength(20);

                RuleFor(a => a.Address!.Notes).MaximumLength(500);

            });
        }
    }
}