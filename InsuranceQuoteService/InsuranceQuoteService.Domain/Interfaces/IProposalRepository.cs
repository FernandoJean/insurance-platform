using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Models;

namespace InsuranceQuoteService.Domain.Interfaces
{
    public interface IProposalRepository
    {
        Task AddAsync(Proposal proposal);

        Task<Proposal?> GetByIdAsync(Guid id);

        Task<(long, IEnumerable<Proposal>)> ListAsync(Pagination pagination);

        Task<int> UpdateStatusAsync(Guid id, ProposalStatus newStatus);
    }
}