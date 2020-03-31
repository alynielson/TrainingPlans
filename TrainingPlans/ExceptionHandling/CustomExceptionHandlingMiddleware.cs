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
            string message = "Internal Server Error";

            if (exception.GetType() == typeof(RestException))
            {
                var restException = (RestException)exception;
                context.Response.StatusCode = (int)restException.StatusCode;
                message = restException.Message;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return context.Response.WriteAsync(
                new ErrorDetails(message, context.Response.StatusCode).ToJsonString());
        }
    }
}
