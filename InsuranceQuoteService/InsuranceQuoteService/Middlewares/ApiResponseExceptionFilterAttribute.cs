using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace InsuranceQuoteService.Presentation.Middlewares
{
    public sealed class ApiResponseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Chamado quando uma exceção é lançada durante o processamento de uma requisição
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            ObjectResult result;

            var conflictExceptions = new HashSet<Type>
            {
            };

            var notFoundExceptions = new HashSet<Type>
            {
            };

            var UnprocessableEntity = new HashSet<Type>
            {
            };

            if (conflictExceptions.Contains(exception.GetType()))
            {
                result = CreateErrorResponse(exception, (int)HttpStatusCode.Conflict);
            }
            else if (notFoundExceptions.Contains(exception.GetType()))
            {
                result = CreateErrorResponse(exception, (int)HttpStatusCode.NotFound);
            }
            else if (UnprocessableEntity.Contains(exception.GetType()))
            {
                result = CreateErrorResponse(exception, (int)HttpStatusCode.UnprocessableEntity);
            }
            else
            {
                result = CreateErrorResponse(exception, (int)HttpStatusCode.InternalServerError);
            }

            context.Result = result;
            base.OnException(context);
        }

        private static ObjectResult CreateErrorResponse(Exception exception, int statusCode)
        {
            return new ObjectResult(new
            {
                type = exception.GetType().Name,
                message = exception.Message,
                StatusCode = statusCode
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
