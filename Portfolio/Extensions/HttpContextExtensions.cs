using Microsoft.EntityFrameworkCore;

namespace Portfolio.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertNumberOfPages<T>
            (this HttpContext httpContext, IQueryable<T> queryable, int perPage)
        {
            double totalRecords = await queryable.CountAsync();
            double totalPages = Math.Ceiling(totalRecords / perPage);

            httpContext.Response.Headers.Append("NumberOfPages", totalPages.ToString());
        }
    }
}
