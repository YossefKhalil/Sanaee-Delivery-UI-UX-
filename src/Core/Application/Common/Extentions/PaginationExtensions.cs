using Core.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Common.Extentions
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedListAsync<T>
            (
                this IQueryable<T> source,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default
            )
        {
            // 1. حساب الإجمالي في قاعدة البيانات
            var count = await source.CountAsync(cancellationToken);

            // 2. جلب عناصر الصفحة المحددة فقط
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
