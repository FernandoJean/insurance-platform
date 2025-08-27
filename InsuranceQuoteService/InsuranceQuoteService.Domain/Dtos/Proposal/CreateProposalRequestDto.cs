using InsuranceQuoteService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InsuranceQuoteService.Domain.Dtos.Proposal
{
    public sealed class CreateProposalRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; } = null!;

        [Required]
        public InsuranceType InsuranceType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal CoverageAmount { get; set; }
    }
}