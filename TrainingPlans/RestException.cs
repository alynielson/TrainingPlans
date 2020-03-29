using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TrainingPlans
{
    public class RestException : Exception
    {
        public RestException(string message, HttpStatusCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public HttpStatusCode ErrorCode { get; set; }
    }
}
