using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Exceptions;
using InsuranceQuoteService.Domain.Interfaces;

namespace InsuranceQuoteService.Application.UseCases
{
    public sealed class GetProposalByIdUseCase(IProposalRepository proposalRepository) : IGetProposalByIdUseCase
    {
        private readonly IProposalRepository _proposalRepository = proposalRepository;

        public async Task<ProposalResponseDto?> ExecuteAsync(Guid id)
        {
            var proposal = await _proposalRepository.GetByIdAsync(id) ?? throw new ProposalIdNotFoundException(id);

            return new ProposalResponseDto
            {
                Id = proposal.Id,
                CustomerName = proposal.CustomerName,
                InsuranceType = proposal.InsuranceType,
                CoverageAmount = proposal.CoverageAmount,
                Status = proposal.Status,
                CreatedAt = proposal.CreatedAt,
                UpdatedAt = proposal.UpdatedAt
            };
        }
    }
}