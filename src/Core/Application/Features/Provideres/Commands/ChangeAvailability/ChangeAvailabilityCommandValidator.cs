

using FluentValidation;

namespace Core.Application.Features.Provideres.Commands.ChangeAvailability
{
    public class ChangeAvailabilityCommandValidator : AbstractValidator<ChangeAvailabilityCommand>
    {
        public ChangeAvailabilityCommandValidator()
        {
            RuleFor(p => p.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");
        }
    }
}
