
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// UnauthorizedException is a custom exception class to handle Unauthorize request.
    /// </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public string Details { get; }

        #region Constructors/Destructors

        /// <summary>
        /// Initializes an instance of UnauthorizedException.
        /// </summary> 
        public UnauthorizedException()
        {

        }

        /// <summary>
        /// Initializes an instance of UnauthorizedException with message.
        /// </summary> 
        /// <param name="message"></param>
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of UnauthorizedException with message and details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public UnauthorizedException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of UnauthorizedException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
