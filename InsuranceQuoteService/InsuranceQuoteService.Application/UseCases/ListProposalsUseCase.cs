using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Interfaces;
using InsuranceQuoteService.Domain.Models;

namespace InsuranceQuoteService.Application.UseCases
{
    public sealed class ListProposalsUseCase : IListProposalsUseCase
    {
        private readonly IProposalRepository _proposalRepository;

        public ListProposalsUseCase(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public async Task<PageModel<ProposalResponseDto>> ExecuteAsync(Pagination pagination, CancellationToken ctx)
        {
            var (count, page) = await _proposalRepository.ListAsync(pagination, ctx);

            var dtoPage = page.Select(p => new ProposalResponseDto
            {
                Id = p.Id,
                CustomerName = p.CustomerName,
                InsuranceType = p.InsuranceType,
                CoverageAmount = p.CoverageAmount,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return new PageModel<ProposalResponseDto>(count, dtoPage, pagination.PageIndex, pagination.PageSize);
        }
    }
}