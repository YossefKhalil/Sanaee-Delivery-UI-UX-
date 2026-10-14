

using FluentValidation;

namespace Core.Application.Features.ProviderDocuments.Commands.ChangeDocumentStatus
{
    public class ChangeDocumentStatusCommandValidator : AbstractValidator<ChangeDocumentStatusCommand>
    {
        public ChangeDocumentStatusCommandValidator()
        {
            RuleFor(d => d.DocumentId)
                .NotEmpty().WithMessage("Provider Id is required!");

            RuleFor(d => d.status)
                .IsInEnum().WithMessage("Invalid Document Status!");

            RuleFor(d => d.AdminId)
                .NotEmpty().WithMessage("Admin Id is required!");
        }
    }
}
