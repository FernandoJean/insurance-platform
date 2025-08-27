namespace InsuranceContractService.Infrastructure.Adapters.Dtos
{
    public sealed class ProposalResponseDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string InsuranceType { get; set; }
        public decimal CoverageAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}