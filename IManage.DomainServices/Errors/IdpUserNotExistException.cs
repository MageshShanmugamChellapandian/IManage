
using IManage.ErrorHandling.ApiExceptions;

namespace IManage.DomainServices.Errors
{
    /// <summary>
    /// Represents the exception used when the Idp user not exist in Simit+.
    /// </summary>
    [Serializable]
    public class IdpUserNotExistException : NotFoundException
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserNotExistException"/> class.
        /// </summary>
        public IdpUserNotExistException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserNotExistException"/> class.
        /// </summary>
        /// <param name="message">The used to set the title info in the response.</param>
        public IdpUserNotExistException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserNotExistException"/> class.
        /// </summary>
        /// <param name="message">The used to set the title info in the response.</param>
        /// <param name="details">The used to set the details info in the response.</param>
        public IdpUserNotExistException(string message,string details) : base(message, details)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserNotExistException"/> class with message and execption.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <remarks>
        /// Create constructor in NotFoundException to take both message and innerException params.
        /// Pass both the params to base().
        /// </remarks>
        public IdpUserNotExistException(string message, Exception innerException) : base(message)
        {
        }

    }
}
