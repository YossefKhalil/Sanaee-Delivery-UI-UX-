using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Pymentes.Dtos;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Pymentes.Queries.GetAllPaymentsByAdmin
{
    public sealed class GetAllPaymentsByAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPaymentsByAdminQuery, PagedResult<GetPaymentDto>>
    {
        public async Task<PagedResult<GetPaymentDto>> Handle(GetAllPaymentsByAdminQuery request, CancellationToken cts)
        {
            // 1. بناء الـ IQueryable الأساسي بدون تنفيذ الاستعلام بعد
            var query = unitOfWork.Repository<Payment>()
                .GetQueryable()
                .IgnoreQueryFilters()
                .AsNoTracking();

            // 2. الترتيب (ضروري جداً لضمان دقة الـ Pagination)
            query = query.OrderByDescending(c => c.CreatedAt);

            // 3. إسقاط البيانات إلى DTO وتطبيق التصفيم في قاعدة البيانات
            var pagedResult = await query
                .Select(c => new GetPaymentDto
                {
                    PaymentId = c.PaymentId,
                    Method = c.Method,
                    Type = c.Type,
                    Status = c.Status,
                    TransactionReference = c.TransactionReference,
                    PaidAt = c.PaidAt,
                    CreatedAt = c.CreatedAt,
                    UserId = c.UserId,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    OrderId = c.OrderId
                })
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
