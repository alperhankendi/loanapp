using System;
using System.Net;
using System.Threading.Tasks;
using Loan.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Loan.Service.WebApi.Middleware
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public CustomErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
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
}