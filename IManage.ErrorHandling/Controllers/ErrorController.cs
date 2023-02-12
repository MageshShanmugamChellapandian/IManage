
using IManage.ErrorHandling.ApiExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace IManage.ErrorHandling.Controllers
{
    /// <summary>
    /// This controller only exists to properly report exceptions to the calling client.
    /// Based on the exception, different responses (e.g. Problem, Status Code) can be
    /// indicated to the client.
    /// </summary>
    [Route("api/error")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ApiValidationException validationException)
            {
                return ValidationProblem(validationException.ValidationErrors);
            }

            if (context.Error is NotFoundException notFoundException)
            {
                return ProblemDetails(details: notFoundException.Details,
                            title: notFoundException.Message, statusCode: HttpStatusCode.NotFound);
            }

            if (context.Error is BadRequestException badRequestException)
            {
                return ProblemDetails(details: badRequestException.Details,
                                title: badRequestException.Message, statusCode: HttpStatusCode.BadRequest);
            }

            if (context.Error is DomainElementAlreadyExistsException existsException)
            {
                return ProblemDetails(details: existsException.Details,
                                title: existsException.Message, statusCode: HttpStatusCode.Conflict);
            }

            if (context.Error is ODataInvalidQueryException exception)
            {
                return ProblemDetails(details: exception.Details,
                                title: exception.Message, statusCode: HttpStatusCode.BadRequest);
            }

            if (context.Error is InternalServerException internalServerException)
            {
                return ProblemDetails(details: internalServerException.Details,
                                title: internalServerException.Message, statusCode: HttpStatusCode.InternalServerError);
            }

            if (context.Error is UnprocessableEntityException unprocessableEntityException)
            {
                return ProblemDetails(details: unprocessableEntityException.Details,
                                title: unprocessableEntityException.Message, statusCode: HttpStatusCode.UnprocessableEntity);
            }

            if (context.Error is ForbiddenException forbiddenException)
            {
                return ProblemDetails(details: forbiddenException.Details,
                                title: forbiddenException.Message, statusCode: HttpStatusCode.Forbidden);
            }

            if (context.Error is UnauthorizedException unauthorizedException)
            {
                return ProblemDetails(details: unauthorizedException.Details,
                                title: unauthorizedException.Message, statusCode: HttpStatusCode.Unauthorized);
            }

            return Problem(
                detail: "Internal Server Error",
                statusCode: (int)HttpStatusCode.InternalServerError,
                title: context.Error.Message);
        }

        /// <summary>
        /// Creates an ObjectResult that produces a ProblemDetails response.
        /// </summary>
        /// <param name="details"></param>
        /// <param name="title"></param>
        /// <param name="statusCode"></param>
        /// <returns>Customized ProblemDetails</returns>
        private ObjectResult ProblemDetails(string details, string title, HttpStatusCode statusCode)
        {
            if (!string.IsNullOrEmpty(details))
            {
                return Problem(
                            detail: details,
                            title: title, statusCode: (int)statusCode);
            }
            return Problem(title: title, statusCode: (int)statusCode);
        }
    }
}



