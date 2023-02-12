
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// UnprocessableEntityException is a custom exception class to handle UnprocessableEntity.
    /// </summary>
    [Serializable]
    public class UnprocessableEntityException : Exception
    {
        public string Details { get; }

        #region Constructors/Destructors


        /// <summary>
        /// Initializes an instance of UnprocessableEntityException.
        /// </summary>
        public UnprocessableEntityException()
        {

        }
        /// <summary>
        /// Initializes an instance of UnprocessableEntityException with message.
        /// </summary> 
        /// <param name="message"></param>
        public UnprocessableEntityException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of UnprocessableEntityException with message and details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public UnprocessableEntityException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        // This constructor is only needed for serialization and has therefore no direct reference.
        protected UnprocessableEntityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of UnprocessableEntityException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
