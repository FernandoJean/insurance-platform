using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Models;

namespace InsuranceQuoteService.Domain.Interfaces
{
    public interface IProposalRepository
    {
        Task AddAsync(Proposal proposal, CancellationToken ctx);

        Task<Proposal?> GetByIdAsync(Guid id, CancellationToken ctx);

        Task<(long, IEnumerable<Proposal>)> ListAsync(Pagination pagination, CancellationToken ctx);

        Task<int> UpdateStatusAsync(Guid id, ProposalStatus newStatus, CancellationToken ctx);
    }
}