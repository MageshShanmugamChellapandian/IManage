using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// BadRequestException is a custom exception class to handle badrequest.
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        public string Details { get; }

        /// <summary>
        /// Initializes an instance of BadRequestException.
        /// </summary>
        public BadRequestException()
        {

        }

        /// <summary>
        /// Initializes an instance of BadRequestException with custom message.
        /// </summary>
        /// <param name="message"></param>
        public BadRequestException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes an instance of BadRequestException with message & details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        // This constructor is only needed for serialization and has therefore no direct reference.
        protected BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of BadRequestException with message & innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
