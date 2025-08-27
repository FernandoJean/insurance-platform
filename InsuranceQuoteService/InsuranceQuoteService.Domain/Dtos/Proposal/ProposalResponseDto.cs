using InsuranceQuoteService.Domain.Enums;

namespace InsuranceQuoteService.Domain.Dtos.Proposal
{
    public sealed class ProposalResponseDto
    {
        public Guid Id { get; set; }
        public required string CustomerName { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public decimal CoverageAmount { get; set; }
        public ProposalStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}