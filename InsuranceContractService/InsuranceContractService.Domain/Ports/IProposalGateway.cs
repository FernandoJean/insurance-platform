using InsuranceContractService.Domain.Dtos.Proposal;

namespace InsuranceContractService.Domain.Ports
{
    public interface IProposalGateway
    {
        Task<ProposalContractDto> GetProposalByIdAsync(Guid proposalId);
    }
}