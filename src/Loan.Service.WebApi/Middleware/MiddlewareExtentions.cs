using Loan.Service.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MiddlewareExtentions
    {
        
        public static IApplicationBuilder UseCustomErrorHandlingMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<CustomErrorHandlingMiddleware>();
    }
}