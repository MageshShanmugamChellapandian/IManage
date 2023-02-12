
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// NotFoundException is a custom exception class to handle NotFound reponse.
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public string Details { get; }

        #region Constructors/Destructors

        /// <summary>
        /// Initializes an instance of NotFoundException.
        /// </summary>
        public NotFoundException()
        {

        }
        /// <summary>
        /// Initializes an instance of NotFoundException with title.
        /// </summary> 
        /// <param name="message">title</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of NotFoundException with title & details.
        /// </summary>
        /// <param name="message">Title</param>
        /// <param name="details">Details</param>
        public NotFoundException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of NotFoundException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
