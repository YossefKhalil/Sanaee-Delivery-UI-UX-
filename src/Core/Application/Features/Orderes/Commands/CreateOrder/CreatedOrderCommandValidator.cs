using FluentValidation;

namespace Core.Application.Features.Orderes.Commands.CreateOrder
{
    public class CreatedOrderCommandValidator : AbstractValidator<CreatedOrderCommand>
    {
        public CreatedOrderCommandValidator()
        {
            RuleFor(o => o.dto.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");

            RuleFor(o => o.dto.AddressId)
                .NotEmpty().WithMessage("Address Id is required!");

            RuleFor(o => o.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
