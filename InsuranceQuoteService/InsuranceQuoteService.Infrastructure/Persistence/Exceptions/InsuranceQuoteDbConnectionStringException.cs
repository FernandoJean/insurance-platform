using InsuranceQuoteService.Infrastructure.Persistence.Resources;
using System.ComponentModel.DataAnnotations;

namespace InsuranceQuoteService.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    /// Exceção lançada quando a connection string do banco de dados do InsuranceQuoteService não está configurada ou é inválida
    /// </summary>
    public sealed class InsuranceQuoteDbConnectionStringException : ValidationException
    {
        public InsuranceQuoteDbConnectionStringException()
            : base(ExceptionMessages.InsuranceQuoteDbConnectionStringException)
        {
        }
    }
}