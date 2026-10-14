
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Core.Application.Features.Customers.Dtos;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Customers.Queries.GetAllCustomersForAdmin
{
    public sealed class GetAllCustomersForAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCustomersForAdminQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto>> Handle(
            GetAllCustomersForAdminQuery request,
            CancellationToken cancellationToken)
        {
            // 1. بناء الـ IQueryable الأساسي بدون تنفيذ الاستعلام بعد
            var query = unitOfWork.Repository<User>()
                .GetQueryable()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(u => u.Type == UserType.Customer);

            // 2. تطبيق الفلترة إذا وُجدت
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim();
                query = query.Where(c => c.FirstName.Contains(search) ||
                                         c.LastName.Contains(search) ||
                                         c.Email.Contains(search));
            }

            // 3. الترتيب (ضروري جداً لضمان دقة الـ Pagination)
            query = query.OrderByDescending(c => c.CreatedAt);

            // 4. إسقاط البيانات إلى DTO وتطبيق التصفيم في قاعدة البيانات
            var pagedResult = await query
                .Select(c => new UserDto
                {
                    UserId = c.UserId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    AverageRating = c.AverageRating,
                    TotalReviewCount = c.TotalReviewCount,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt
                })
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return pagedResult;
        }
    }
}
