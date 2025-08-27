using InsuranceQuoteService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace InsuranceQuoteService.Presentation.Middlewares
{
    /// <summary>
    /// Captura exceções da API e retorna respostas HTTP padronizadas.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
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

            var notFoundExceptions = new HashSet<Type>
            {
                typeof(ProposalIdNotFoundException)
            };

            if (notFoundExceptions.Contains(exception.GetType()))
            {
                result = CreateErrorResponse(exception, (int)HttpStatusCode.NotFound);
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