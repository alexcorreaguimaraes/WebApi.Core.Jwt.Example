using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Core.Jwt.Example.Models;

namespace WebApi.Core.Jwt.Example.Extensions
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorResultModel result = new ErrorResultModel();
            List<string> mensagem = new List<string>();
            mensagem.Add("Error Cod:");
            string error = exception.Message + " - " + exception.InnerException;
            result.Error = error;
            result.Messages = mensagem;
            result.StatusCode = context.Response.StatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
