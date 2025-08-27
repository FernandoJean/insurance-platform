using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Entities;

namespace InsuranceQuoteService.Application.Interfaces
{
    public interface ICreateProposalUseCase
    {
        Task<ProposalResponseDto> ExecuteAsync(CreateProposalRequestDto createProposalRequestDto, CancellationToken ctx);
    }
}