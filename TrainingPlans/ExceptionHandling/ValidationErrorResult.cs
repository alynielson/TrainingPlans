using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;

namespace TrainingPlans.ExceptionHandling
{
    public class ValidationErrorResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var errors = modelStateEntries?.SelectMany(x => x.Value?.Errors)
                .Select(x => x?.ErrorMessage)
                .Where(x => !(x is null)).ToList();

            var problemDetails = new ErrorDetails(errors, 400);

            context.HttpContext.Response.WriteAsync(problemDetails.ToJsonString<ErrorDetails>());
            return Task.CompletedTask;
        }
    }
}
