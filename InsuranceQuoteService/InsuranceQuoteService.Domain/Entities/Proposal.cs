using InsuranceQuoteService.Domain.Enums;

namespace InsuranceQuoteService.Domain.Entities
{
    public sealed class Proposal
    {
        public Proposal()
        { }

        public Guid Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public InsuranceType InsuranceType { get; set; }
        public decimal CoverageAmount { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}