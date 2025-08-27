using InsuranceQuoteService.Domain.Enums;

namespace InsuranceQuoteService.Application.Interfaces
{
    public interface IUpdateProposalsStatusUseCase
    {
        Task ExecuteAsync(Guid id, ProposalStatus newStatus, CancellationToken ctx);
    }
}