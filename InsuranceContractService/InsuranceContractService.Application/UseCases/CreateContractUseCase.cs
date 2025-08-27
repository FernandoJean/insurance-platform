using InsuranceContractService.Application.Exceptions;
using InsuranceContractService.Application.Interfaces;
using InsuranceContractService.Domain.Dtos.Contract;
using InsuranceContractService.Domain.Entities;
using InsuranceContractService.Domain.Enums;
using InsuranceContractService.Domain.Interfaces;
using InsuranceContractService.Domain.Ports;

namespace InsuranceContractService.Application.UseCases
{
    public sealed class CreateContractUseCase(IContractRepository contractRepository, IProposalGateway proposalGateway) : ICreateContractUseCase
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IProposalGateway _proposalGateway = proposalGateway;

        public async Task<ContractResponseDto> ExecuteAsync(Guid proposalId)
        {
            var proposal = await _proposalGateway.GetProposalByIdAsync(proposalId);

            if (proposal.Status is not ProposalStatus.Approved)
            {
                throw new ProposalNotApprovedException(proposalId);
            }

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                ProposalId = proposal.Id,
                ContractedAt = DateTime.UtcNow
            };

            await _contractRepository.AddAsync(contract);

            return new ContractResponseDto
            {
                Id = contract.Id,
                ProposalId = contract.ProposalId,
                ContractedAt = contract.ContractedAt
            };
        }
    }
}