using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ExceptionHandling
{
    public class ErrorDetails
    {
        public List<string> Messages { get; set; }
        public int ErrorCode { get; set; }

        public ErrorDetails(string message, int errorCode)
        {
            Messages = new List<string> { message };
            ErrorCode = errorCode;
        }

        public ErrorDetails(List<string> messages, int errorCode)
        {
            Messages = messages;
            ErrorCode = errorCode;
        }
    }
}
