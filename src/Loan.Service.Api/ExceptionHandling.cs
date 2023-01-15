using System.Net;
using Loan.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection;

public class CustomErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger _logger;

    public CustomErrorHandlingMiddleware(RequestDelegate next, ILogger<CustomErrorHandlingMiddleware> logger)
    {
        this.next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment env)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            await HandleExceptionAsync(context, ex, env);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
    {
        var code = HttpStatusCode.InternalServerError;
        if (ex is BusinessException)
        {
            switch (ex)
            {
                case LoanApplicationNotFound:
                case OperatorNotFound:
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    break;
            }
        }
        var problem = new ProblemDetails
        {
            Status = (int?)code,
            Title = ex.Message,
            Detail = ex.ToString(),
            Type = ex.GetType().ToString()
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;

        var result = JsonConvert.SerializeObject(problem);
        return context.Response.WriteAsync(result);
    }
}