

using Core.Application.Features.Addresses.Dtos;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Application.Features.ProviderDocuments.Dtos;
using MediatR;

namespace Core.Application.Features.Provideres.Commands.CreateProvider
{
    public sealed record CreateProviderCommand(

        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        string OtpCode,
        string NationalNumber,
        IReadOnlyList<ProviderDocumentDto> ProviderDocuments,
        IReadOnlyList<ProviderCategoryDto> ProviderCategories,
        CreateAddressByUserEntity? Address = null

        ) : IRequest<int>;
}
