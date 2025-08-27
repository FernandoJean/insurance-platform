using InsuranceQuoteService.Application.UseCases;
using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Exceptions;
using InsuranceQuoteService.Domain.Interfaces;
using Moq;

namespace InsuranceQuoteService.UnitTests.UseCases
{
    public class GetProposalByIdUseCaseTests
    {
        private readonly Mock<IProposalRepository> _proposalRepositoryMock;
        private readonly GetProposalByIdUseCase _useCase;

        public GetProposalByIdUseCaseTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _useCase = new GetProposalByIdUseCase(_proposalRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ProposalExists_ReturnsProposalResponseDto()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            var proposal = new Proposal
            {
                Id = proposalId,
                CustomerName = "Test Customer",
                InsuranceType = InsuranceType.Health,
                CoverageAmount = 100m,
                Status = ProposalStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _proposalRepositoryMock
                .Setup(r => r.GetByIdAsync(proposalId))
                .ReturnsAsync(proposal);

            // Act
            var result = await _useCase.ExecuteAsync(proposalId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(proposal.Id, result!.Id);
            Assert.Equal(proposal.CustomerName, result.CustomerName);
            Assert.Equal(proposal.InsuranceType, result.InsuranceType);
            Assert.Equal(proposal.CoverageAmount, result.CoverageAmount);
            Assert.Equal(proposal.Status, result.Status);
            Assert.Equal(proposal.CreatedAt, result.CreatedAt);
            Assert.Equal(proposal.UpdatedAt, result.UpdatedAt);
        }

        [Fact]
        public async Task ExecuteAsync_ProposalDoesNotExist_ThrowsProposalIdNotFoundException()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            _proposalRepositoryMock
                .Setup(r => r.GetByIdAsync(proposalId))
                .ReturnsAsync((Proposal?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ProposalIdNotFoundException>(() => _useCase.ExecuteAsync(proposalId));
            Assert.Equal(proposalId, exception.Id);
        }
    }
}
