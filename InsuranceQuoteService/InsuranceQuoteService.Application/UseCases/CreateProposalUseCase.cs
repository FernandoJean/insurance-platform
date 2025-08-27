using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Interfaces;

namespace InsuranceQuoteService.Application.UseCases
{
    public sealed class CreateProposalUseCase : ICreateProposalUseCase
    {
        private readonly IProposalRepository _proposalRepository;

        public CreateProposalUseCase(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public async Task<ProposalResponseDto> ExecuteAsync(CreateProposalRequestDto createProposalRequestDto, CancellationToken ctx)
        {
            var proposal = new Proposal
            {
                Id = Guid.NewGuid(),
                CustomerName = createProposalRequestDto.CustomerName,
                InsuranceType = createProposalRequestDto.InsuranceType,
                CoverageAmount = createProposalRequestDto.CoverageAmount,
                Status = ProposalStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _proposalRepository.AddAsync(proposal, ctx);

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