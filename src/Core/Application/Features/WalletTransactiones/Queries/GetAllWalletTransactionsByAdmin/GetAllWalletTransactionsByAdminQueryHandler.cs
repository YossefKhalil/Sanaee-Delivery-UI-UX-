using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Core.Application.Features.Pymentes.Dtos;
using Core.Application.Features.WalletTransactiones.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Features.WalletTransactiones.Queries.GetAllWalletTransactionsByAdmin
{
    public class GetAllWalletTransactionsByAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllWalletTransactionsByAdminQuery, PagedResult<GetWalletTransactionDto>>
    {
        public async Task<PagedResult<GetWalletTransactionDto>> Handle(GetAllWalletTransactionsByAdminQuery request, CancellationToken cts)
        {
            // 1. بناء الـ IQueryable الأساسي بدون تنفيذ الاستعلام بعد
            var query = unitOfWork.Repository<WalletTransaction>()
                .GetQueryable()
                .IgnoreQueryFilters()
                .AsNoTracking();

            // 2. الترتيب (ضروري جداً لضمان دقة الـ Pagination)
            query = query.OrderByDescending(c => c.CreatedAt);

            // 3. إسقاط البيانات إلى DTO وتطبيق التصفيم في قاعدة البيانات
            var pagedResult = await query
                .Select(c => new GetWalletTransactionDto
                {
                    TransactionId = c.TransactionId,
                    Amount = c.Amount,
                    Description = c.Description,
                    WalletId = c.WalletId,
                    Type = c.Type,
                    CreatedAt = c.CreatedAt,
                    OrderId = c.OrderId
                })
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
