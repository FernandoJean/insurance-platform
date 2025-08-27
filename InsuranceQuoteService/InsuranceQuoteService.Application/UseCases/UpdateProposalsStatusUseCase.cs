using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Exceptions;
using InsuranceQuoteService.Domain.Interfaces;

namespace InsuranceQuoteService.Application.UseCases
{
    public sealed class UpdateProposalsStatusUseCase(IProposalRepository proposalRepository) : IUpdateProposalsStatusUseCase
    {
        private readonly IProposalRepository _proposalRepository = proposalRepository;

        public async Task ExecuteAsync(Guid id, ProposalStatus newStatus)
        {
            var rowsAffected = await _proposalRepository.UpdateStatusAsync(id, newStatus);

            if (rowsAffected == 0)
            {
                throw new ProposalIdNotFoundException(id);
            }
        }
    }
}