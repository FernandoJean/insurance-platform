using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Models;

namespace InsuranceQuoteService.Application.Interfaces
{
    public interface IListProposalsUseCase
    {
        Task<PageModel<ProposalResponseDto>> ExecuteAsync(Pagination pagination);
    }
}