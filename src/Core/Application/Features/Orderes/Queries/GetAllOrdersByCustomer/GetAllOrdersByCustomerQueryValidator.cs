
using FluentValidation;

namespace Core.Application.Features.Orderes.Queries.GetAllOrdersByCustomer
{
    public class GetAllOrdersByCustomerQueryValidator : AbstractValidator<GetAllOrdersByCustomerQuery>
    {
        public GetAllOrdersByCustomerQueryValidator()
        {
            RuleFor(o => o.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required!");
        }
    }
}
