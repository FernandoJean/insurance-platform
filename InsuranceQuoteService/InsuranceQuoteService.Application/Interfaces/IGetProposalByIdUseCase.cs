using InsuranceQuoteService.Domain.Dtos.Proposal;

namespace InsuranceQuoteService.Application.Interfaces
{
    public interface IGetProposalByIdUseCase
    {
        Task<ProposalResponseDto?> ExecuteAsync(Guid id, CancellationToken ctx);
    }
}