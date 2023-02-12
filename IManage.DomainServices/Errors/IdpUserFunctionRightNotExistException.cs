
using IManage.ErrorHandling.ApiExceptions;

namespace IManage.DomainServices.Errors
{
    /// <summary>
    /// Represents the exception used when the FunctionRights not exist in Simit+ for Idp user.
    /// </summary>
    [Serializable]
    public class IdpUserFunctionRightNotExistException : NotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserFunctionRightNotExistException"/> class.
        /// </summary>
        public IdpUserFunctionRightNotExistException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserFunctionRightNotExistException"/> class.
        /// </summary>
        /// <param name="message">Used to set the tile info in the response.</param>
        public IdpUserFunctionRightNotExistException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserFunctionRightNotExistException"/> class.
        /// </summary>
        /// <param name="message">The used to set the title info in the response.</param>
        /// <param name="details">The used to set the details info in the response.</param>
        public IdpUserFunctionRightNotExistException(string message, string details) : base(message, details)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdpUserFunctionRightNotExistException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <remarks>
        /// Create constructor in NotFoundException to take both message and innerException params.
        /// Pass both the params to base().
        /// </remarks>
        public IdpUserFunctionRightNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        
    }
}
