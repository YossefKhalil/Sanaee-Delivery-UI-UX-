
using FluentValidation;

namespace Core.Application.Features.ProviderDocuments.Commands.CreateDocument
{
    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator()
        {
            RuleFor(d => d.dto.Type)
                .IsInEnum().WithMessage("Invalid Document Type!");

            RuleFor(d => d.dto.DocumentUrl)
                .NotEmpty().WithMessage("Document Url is required!");

            RuleFor(d => d.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");
        }
    }
}
