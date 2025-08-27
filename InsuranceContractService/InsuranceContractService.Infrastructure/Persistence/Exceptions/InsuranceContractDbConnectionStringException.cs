using InsuranceContractService.Infrastructure.Persistence.Resources;
using System.ComponentModel.DataAnnotations;

namespace InsuranceContractService.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    /// Exceção lançada quando a connection string do banco de dados do InsuranceContractService não está configurada ou é inválida
    /// </summary>
    public sealed class InsuranceContractDbConnectionStringException : ValidationException
    {
        public InsuranceContractDbConnectionStringException()
            : base(ExceptionMessages.InsuranceContractDbConnectionStringException)
        {
        }
    }
}