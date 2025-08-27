using InsuranceContractService.Domain.Dtos.Contract;

namespace InsuranceContractService.Application.Interfaces
{
    public interface ICreateContractUseCase
    {
        Task<ContractResponseDto> ExecuteAsync(Guid proposalId);
    }
}