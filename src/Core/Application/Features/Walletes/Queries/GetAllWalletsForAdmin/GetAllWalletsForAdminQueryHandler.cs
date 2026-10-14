

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Walletes.Dtos;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Core.Application.Features.Walletes.Queries.GetAllWalletsForAdmin
{
    public class GetAllWalletsForAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllWalletsForAdminQuery, PagedResult<WalletDto>>
    {
        public async Task<PagedResult<WalletDto>> Handle(GetAllWalletsForAdminQuery request, CancellationToken cts)
        {
            var query = unitOfWork.Repository<Wallet>()
                .GetQueryable()
                .AsNoTracking();

            query = query.OrderByDescending(c => c.CreatedAt);

            var pagedResult = await query
                .Select(c => new WalletDto
                {
                    WalletId = c.WalletId,
                    Balance = c.Balance,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsActive = c.IsActive
                })
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
