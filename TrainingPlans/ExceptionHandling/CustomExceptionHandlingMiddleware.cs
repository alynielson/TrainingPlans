using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TrainingPlans.Common;

namespace TrainingPlans.ExceptionHandling
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // TODO: log something
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            List<string> messages;

            if (exception.GetType() == typeof(RestException))
            {
                var restException = (RestException)exception;
                context.Response.StatusCode = (int)restException.StatusCode;
                messages = new List<string> { restException.Message };
            }
            else if (exception.GetType() == typeof(InvalidModelException))
            {
                var modelException = (InvalidModelException)exception;
                context.Response.StatusCode = (int)modelException.StatusCode;
                messages = modelException.ErrorMessages.ToList();
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                messages = new List<string> { exception.Message };
            }

            return context.Response.WriteAsync(
                new ErrorResponseMessage(context.Response.StatusCode, messages).ToJsonString(), System.Text.Encoding.UTF8);
        }
    }
}
