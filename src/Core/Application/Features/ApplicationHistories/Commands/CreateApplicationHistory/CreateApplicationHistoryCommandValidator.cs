using Core.Domain.Entities;
using FluentValidation;


namespace Core.Application.Features.ApplicationHistories.Commands.CreateApplicationHistory
{
    public class CreateApplicationHistoryCommandValidator : AbstractValidator<CreateApplicationHistoryCommand>
    {
        public CreateApplicationHistoryCommandValidator()
        {
            RuleFor(ah => ah.dto.Status)
                .IsInEnum().WithMessage("Application Status is Invalid!");

            RuleFor(ah => ah.dto.Notes).MaximumLength(500);

            RuleFor(ah => ah.dto.ProviderId)
                .NotEmpty().WithMessage("Provider Id is Required!");

            RuleFor(ah => ah.ChangedBy)
                .NotEmpty().WithMessage("Admin Id is Required!");
        }
    }
}
