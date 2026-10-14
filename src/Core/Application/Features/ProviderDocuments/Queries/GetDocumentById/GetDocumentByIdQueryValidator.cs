
using FluentValidation;

namespace Core.Application.Features.ProviderDocuments.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryValidator : AbstractValidator<GetDocumentByIdQuery>
    {

        public GetDocumentByIdQueryValidator()
        {
            RuleFor(d => d.DocumentId)
                .NotEmpty().WithMessage("Document Id is required!");
        }
    }
}
