

using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Features.ProviderStatusHistories.Commands.CreateProviderStatusHistory
{
    public class CreateProviderStatusHistoryCommandValidator : AbstractValidator<CreateProviderStatusHistoryCommand>
    {
        public CreateProviderStatusHistoryCommandValidator()
        {
            RuleFor(psh => psh.dto.Status)
                .IsInEnum().WithMessage("Status is invalid!");

            RuleFor(psh => psh.dto.Notes).MaximumLength(500);

            RuleFor(psh => psh.dto.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");

            RuleFor(psh => psh.AdminId)
                .NotEmpty().WithMessage("Admin Id is required!");
        }
    }
}
