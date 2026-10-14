
using Core.Application.Features.ApplicationHistories.Dtos;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Application.Features.ProviderDocuments.Dtos;
using FluentValidation;

namespace Core.Application.Features.Provideres.Commands.CreateProvider
{
    public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
    {
        public CreateProviderCommandValidator()
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


            RuleFor(p => p.NationalNumber)
                .NotEmpty().WithMessage("National Number is required!")
                .Matches(@"^[0-9]{14}$").WithMessage("National Number must not exceed 14 character!");

            RuleFor(p => p.ProviderDocuments)
                .NotEmpty().WithMessage("Provider Document is required!");

            RuleForEach(x => x.ProviderDocuments)
            .SetValidator(new ProviderDocumentDtoValidator());

            RuleFor(x => x.ProviderCategories)
                .NotEmpty().WithMessage("Provider Category is required!");

            RuleForEach(x => x.ProviderCategories)
                .SetValidator(new ProviderCategoryDtoValidator());

        }
    }

    public class ProviderDocumentDtoValidator : AbstractValidator<ProviderDocumentDto>
    {
        public ProviderDocumentDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Invalid Document Type!");


            RuleFor(x => x.DocumentUrl)
                .NotEmpty().WithMessage("Invalid Document Url!");
        }
    }

    public class ProviderCategoryDtoValidator : AbstractValidator<ProviderCategoryDto>
    {
        public ProviderCategoryDtoValidator()
        {
            RuleFor(x => x.YearsOfExperience)
                .GreaterThan(0).WithMessage("Years Of Experience is required!");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category Id is required!");
        }
    }
}
