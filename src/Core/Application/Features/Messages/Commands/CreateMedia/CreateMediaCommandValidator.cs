using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Features.Messages.Commands.CreateMedia
{
    public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
    {
        public CreateMediaCommandValidator()
        {
            RuleFor(m => m.SenderId)
                .NotEmpty().WithMessage("Sender Id is required!");

            RuleFor(m => m.dto.RecipientId)
                .NotEmpty().WithMessage("Recipient Id is required!");

            RuleFor(m => m.dto.Type)
                .NotEmpty().WithMessage("Message Type is required!");

            RuleFor(m => m.dto.AttachmentUrl)
                .NotEmpty().WithMessage("The Attachment Url is required!");

            RuleFor(m => m.dto.Content)
                .MaximumLength(1000).WithMessage("Text Exceeded the  limit!");


        }
    }
}
