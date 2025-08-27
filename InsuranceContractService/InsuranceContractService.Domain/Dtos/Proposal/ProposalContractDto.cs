using InsuranceContractService.Domain.Enums;

namespace InsuranceContractService.Domain.Dtos.Proposal
{
    public sealed class ProposalContractDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public decimal CoverageAmount { get; set; }
        public ProposalStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}