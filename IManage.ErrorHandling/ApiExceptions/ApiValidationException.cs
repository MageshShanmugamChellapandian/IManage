using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// ApiValidationException is a custom exception class to handle fluentvalidation.
    /// </summary>
    [Serializable]
    public class ApiValidationException : Exception
    {
        /// <summary>
        /// Initializes an instance of ApiValidationException.
        /// </summary>
        public ApiValidationException()
            : base("One or more validation failures have occurred.")
        {
            ValidationErrors = new ModelStateDictionary();
        }

        /// <summary>
        /// Initializes an instance of ApiValidationException with validationerrors.
        /// </summary>
        /// <param name="validationErrors">Errors</param>
        public ApiValidationException(ModelStateDictionary validationErrors)
            : this()
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Initializes an instance of ApiValidationException with custom message.
        /// </summary>
        /// <param name="message"></param>
        public ApiValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of ApiValidationException with validationerrors & custom message.
        /// </summary>
        /// <param name="validationErrors"></param>
        public ApiValidationException(ModelStateDictionary validationErrors, string message)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Initializes an instance of ApiValidationException with custom message and innerException details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ApiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// The Validation Errors
        /// </summary>
        public ModelStateDictionary ValidationErrors { get; }
    }
}
