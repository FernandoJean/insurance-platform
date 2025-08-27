using InsuranceContractService.Domain.Entities;

namespace InsuranceContractService.Domain.Interfaces
{
    public interface IContractRepository
    {
        Task AddAsync(Contract contract);
    }
}