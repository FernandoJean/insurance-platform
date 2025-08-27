using InsuranceContractService.Infrastructure.Persistence.Resources;
using System.ComponentModel.DataAnnotations;

namespace InsuranceContractService.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    /// Exception lançada quando a URL base da API de Proposals não está configurada no appsettings.json.
    /// </summary>
    public sealed class ProposalApiBaseUrlNotConfiguredException : ValidationException
    {
        public ProposalApiBaseUrlNotConfiguredException()
            : base(ExceptionMessages.ProposalApiBaseUrlNotConfiguredException)
        {
        }
    }
}