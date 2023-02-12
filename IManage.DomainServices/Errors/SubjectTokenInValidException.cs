
using IManage.ErrorHandling.ApiExceptions;

namespace IManage.DomainServices.Errors
{
    /// <summary>
    /// Represents the exception used when the subject token is invalid .
    /// </summary>
    [Serializable]
    public class SubjectTokenInValidException : BadRequestException
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTokenInValidException"/> class.
        /// </summary>
        public SubjectTokenInValidException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTokenInValidException"/> class.
        /// </summary>
        /// <param name="message">The used to set the title info in the response.</param>
        public SubjectTokenInValidException(string message):base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTokenInValidException"/> class.
        /// </summary>
        /// <param name="message">The used to set the title info in the response</param>
        /// <param name="details">The used to set the details info in the response</param>
        public SubjectTokenInValidException(string message,string details) : base(message, details)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTokenInValidException"/> class with message and execption.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <remarks>
        /// Create constructor in BadRequestException to take both message and innerException params.
        /// Pass both the params to base().
        /// </remarks>
        public SubjectTokenInValidException(string message, Exception innerException) : base(message)
        {
        }


    }
}
