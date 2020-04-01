using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            ProblemDetails result;

            if (exception.GetType() == typeof(RestException))
            {
                var restException = (RestException)exception;
                context.Response.StatusCode = (int)restException.StatusCode;
                result = new ProblemDetails
                {
                    Detail = restException.Message,
                    Status = context.Response.StatusCode,
                    Title = "There was an error in the request."
                };
            }
            else if (exception.GetType() == typeof(InvalidModelException))
            {
                var modelException = (InvalidModelException)exception;
                context.Response.StatusCode = (int)modelException.StatusCode;
                result = new ValidationProblemDetails(modelException.ErrorMessages)
                {
                    Status = context.Response.StatusCode,
                    Title = "One or more validation errors occurred."
                };
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = new ProblemDetails
                {
                    Detail = exception.Message,
                    Status = context.Response.StatusCode,
                    Title = "An internal server errror has occurred."
                };
            }

            return context.Response.WriteAsync(
              result.ToJsonString(result.GetType()), System.Text.Encoding.UTF8);
        }
    }
}
