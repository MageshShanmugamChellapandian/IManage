
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// ForbiddenException is a custom exception class to handle Forbidden request.
    /// </summary>
    [Serializable]
    public class ForbiddenException : Exception
    {
        public string Details { get; }

        #region Constructors/Destructors

        /// <summary>
        /// Initializes an instance of ForbiddenException.
        /// </summary>
        public ForbiddenException()
        {

        }

        /// <summary>
        /// Initializes an instance of ForbiddenException with message.
        /// </summary> 
        /// <param name="message">title</param>
        public ForbiddenException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of ForbiddenException with message & details.
        /// </summary>
        /// <param name="message">title</param>
        /// <param name="details">details</param>
        public ForbiddenException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of ForbiddenException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
