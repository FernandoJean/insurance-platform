using InsuranceContractService.Domain.Dtos.Proposal;
using InsuranceContractService.Domain.Enums;
using InsuranceContractService.Infrastructure.Adapters.Dtos;

namespace InsuranceContractService.Infrastructure.Adapters.Mappers
{
    public static class ProposalMapper
    {
        public static ProposalContractDto ToContractDto(ProposalResponseDto response)
        {
            return new ProposalContractDto
            {
                Id = response.Id,
                CustomerName = response.CustomerName,
                InsuranceType = Enum.Parse<InsuranceType>(response.InsuranceType),
                CoverageAmount = response.CoverageAmount,
                Status = Enum.Parse<ProposalStatus>(response.Status),
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt
            };
        }
    }
}