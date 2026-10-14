using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Features.Messages.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(m => m.SenderId)
                .NotEmpty().WithMessage("Sender Id is required!");
        }
    }
}
