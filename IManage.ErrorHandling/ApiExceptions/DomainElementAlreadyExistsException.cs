
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// DomainElementAlreadyExistsException is a custom exception class to handle conflict respectively.
    /// </summary>
    [Serializable]
    public class DomainElementAlreadyExistsException : Exception
    {
        public string Details { get; }

        /// <summary>
        /// Initializes a new instance of the DomainElementAlreadyExistsException.
        /// </summary>
        public DomainElementAlreadyExistsException()
            : base("The same object already exists!")
        {

        }

        /// <summary>
        /// Initializes a new instance of the DomainElementAlreadyExistsException class with a custom message.
        /// </summary>
        /// <param name="message">title</param>
        public DomainElementAlreadyExistsException(string message)
             : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the DomainElementAlreadyExistsException class with a custom message and details.
        /// </summary>
        /// <param name="message">title</param>
        /// <param name="details">details</param>
        public DomainElementAlreadyExistsException(string message, string details)
             : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DomainElementAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes an instance of DomainElementAlreadyExistsException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainElementAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
