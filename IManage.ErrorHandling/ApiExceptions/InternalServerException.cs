
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// InternalServerException is a custom exception class to handle InternalServerException.
    /// </summary>
    [Serializable]
    public class InternalServerException : Exception
    {
        public string Details { get; }

        /// <summary>
        /// Initializes a new instance of the InternalServerException class.
        /// </summary>
        public InternalServerException()
        {
        }

        /// <summary>
        /// Initializes an instance of InternalServerException with a custom message.
        /// </summary> 
        /// <param name="message"></param>
        public InternalServerException(string message)
       : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of InternalServerException with a custom message and details.
        /// </summary> 
        /// <param name="message">title</param>
        /// <param name="details">details</param>

        public InternalServerException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// Initializes a new instance of the InternalServerException class with a custom message and inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InternalServerException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InternalServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
