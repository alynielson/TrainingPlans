using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ExceptionHandling
{
    public class ErrorResponseMessage
    {
        public int StatusCode { get; }
        public List<string> Messages { get; }

        public ErrorResponseMessage(int statusCode, List<string> messages)
        {
            StatusCode = statusCode;
            Messages = messages;
        }
    }
}
