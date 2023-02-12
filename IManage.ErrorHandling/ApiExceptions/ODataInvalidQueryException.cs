
using System;
using System.Runtime.Serialization;

namespace IManage.ErrorHandling.ApiExceptions
{
    /// <summary>
    /// ODataInvalidQueryException is a custom exception class to handle OData exceptions.
    /// </summary>
    [Serializable]
    public class ODataInvalidQueryException : Exception
    {
        public string Details { get; }

        /// <summary>
        /// Initializes an instance of ODataInvalidQueryException.
        /// </summary>
        public ODataInvalidQueryException()
        {
        }

        /// <summary>
        /// Initializes an instance of ODataInvalidQueryException with  title.
        /// </summary>
        /// <param name="msg">title of the excpetion</param>
        public ODataInvalidQueryException(string msg)
        : base(msg)
        {
        }


        /// <summary>
        /// Initializes an instance of ODataInvalidQueryException with  title & details.
        /// </summary>
        /// <param name="msg">Title</param>
        /// <param name="details">Details</param>
        public ODataInvalidQueryException(string msg, string details)
        : base(msg)
        {
            Details = details;
        }

        /// <summary>
        /// Initializes an instance of ODataInvalidQueryException with  message & exception.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public ODataInvalidQueryException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
        /// <summary>
        /// This constructor is only needed for serialization and has therefore no direct reference.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ODataInvalidQueryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
