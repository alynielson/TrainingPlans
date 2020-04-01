using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ExceptionHandling
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(IDictionary<string, string[]> messages) : base()
        {
            ErrorMessages = messages;
        }
        public IDictionary<string, string[]> ErrorMessages { get; }
        public System.Net.HttpStatusCode StatusCode { get; } = System.Net.HttpStatusCode.BadRequest;
    }
}
