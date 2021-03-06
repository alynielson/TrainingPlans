﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TrainingPlans.ExceptionHandling
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode statusCode, string message) : base(message) 
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
