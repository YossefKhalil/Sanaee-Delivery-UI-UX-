
using Core.Application.Features.ProviderCategories.Dtos;
using MediatR;

namespace Core.Application.Features.ProviderCategories.Queries.GetProviderCategoryById
{
    public sealed record GetProviderCategoryByIdQuery(int ProviderCategoryId) : IRequest<GetProviderCategoryDto>;
}
