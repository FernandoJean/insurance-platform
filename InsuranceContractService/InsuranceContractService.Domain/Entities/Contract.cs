namespace InsuranceContractService.Domain.Entities
{
    public sealed class Contract
    {
        public Guid Id { get; set; }
        public Guid ProposalId { get; set; }
        public DateTime ContractedAt { get; set; }
    }
}