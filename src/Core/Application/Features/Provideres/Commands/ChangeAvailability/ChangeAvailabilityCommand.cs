

using MediatR;

namespace Core.Application.Features.Provideres.Commands.ChangeAvailability
{
    public sealed record ChangeAvailabilityCommand(int ProviderId) : IRequest;

}
