namespace InsuranceContractService.Domain.Dtos.Contract
{
    public sealed class ContractResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProposalId { get; set; }
        public DateTime ContractedAt { get; set; }
    }
}