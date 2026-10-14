

using FluentValidation;

namespace Core.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Customer Id must be greater than 0 !");
        }
    }
}
