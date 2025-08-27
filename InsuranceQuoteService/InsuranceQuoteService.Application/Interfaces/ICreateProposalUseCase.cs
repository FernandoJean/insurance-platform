using InsuranceQuoteService.Domain.Dtos.Proposal;

namespace InsuranceQuoteService.Application.Interfaces
{
    public interface ICreateProposalUseCase
    {
        Task<ProposalResponseDto> ExecuteAsync(CreateProposalRequestDto createProposalRequestDto);
    }
}