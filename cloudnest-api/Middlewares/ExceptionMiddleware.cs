using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using CloudNest.Api.Models.Dtos;
using Serilog;

namespace CloudNest.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine(httpContext.Request);
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ResponseMessages.ExceptionMessage}: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {

            httpContext.Response.ContentType = "application/json";
            var response = new ApiResponse<string>(ResponseMessages.ExceptionMessage);

            _logger.LogError(exception, exception.Message);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
