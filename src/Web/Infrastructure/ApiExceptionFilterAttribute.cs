using Application.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain.Exceptions;

namespace Web.Infrastructure
{

    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(InvalidStringException), HandleInvalidStringException },
        };
        }
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.TryGetValue(type, out var value))
            {
                value.Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleValidationException(context);
            }

            LogAndSetInternalServerError(context);
        }
        private void LogAndSetInternalServerError(ExceptionContext context)
        {
            var exception = context.Exception;

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;
            var details = CreateProblemDetails(exception, HttpStatusCode.BadRequest, "https://tools.ietf.org/html/rfc7231#section-6.5.1", "Se encontraron uno o mas errores de validacion");
         
            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleInvalidStringException(ExceptionContext context)
        {
            var exception = (InvalidStringException)context.Exception;
            var details = CreateProblemDetails(exception, HttpStatusCode.BadRequest, "https://tools.ietf.org/html/rfc7231#section-6.5.1", exception.Message);


            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = (NotFoundException)context.Exception;
            var details = CreateProblemDetails(exception, HttpStatusCode.NotFound, "https://tools.ietf.org/html/rfc7231#section-6.5.4", "Recurso no encontrado");
           

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }
        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            context.ExceptionHandled = true;
        }
        private ProblemDetails CreateProblemDetails(Exception exception, HttpStatusCode status, string type, string title)
        {
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = (int)status,
                Type = type
            };

            var validationException = exception as ValidationException;
            if (validationException?.Errors != null && validationException.Errors.Any())
            {
                problemDetails.Extensions["errors"] = validationException.Errors;
            }

            return problemDetails;
        }
      
    }
}
