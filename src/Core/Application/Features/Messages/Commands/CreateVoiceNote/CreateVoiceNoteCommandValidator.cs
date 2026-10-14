using FluentValidation;

namespace Core.Application.Features.Messages.Commands.CreateVoiceNote
{
    public class CreateVoiceNoteCommandValidator : AbstractValidator<CreateVoiceNoteCommand>
    {
        public CreateVoiceNoteCommandValidator()
        {
            RuleFor(m => m.SenderId)
                .NotEmpty().WithMessage("Sender Id is required!");

            RuleFor(m => m.dto.RecipientId)
                .NotEmpty().WithMessage("Recipient Id is required!");

            RuleFor(m => m.dto.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");

            RuleFor(m => m.dto.AttachmentUrl)
                .NotEmpty().WithMessage("Attachment Url is required!");

            RuleFor(m => m.dto.DurationInSeconds)
                .NotEmpty().WithMessage("Duration In Seconds is required!");
        }
    }
}
