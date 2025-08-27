using InsuranceContractService.Application.Exceptions;
using InsuranceContractService.Application.UseCases;
using InsuranceContractService.Domain.Dtos.Proposal;
using InsuranceContractService.Domain.Entities;
using InsuranceContractService.Domain.Enums;
using InsuranceContractService.Domain.Interfaces;
using InsuranceContractService.Domain.Ports;
using Moq;

namespace InsuranceContractService.UnitTests.UseCases
{
    public class CreateContractUseCaseTests
    {
        private readonly Mock<IContractRepository> _contractRepoMock;
        private readonly Mock<IProposalGateway> _proposalGatewayMock;
        private readonly CreateContractUseCase _useCase;

        public CreateContractUseCaseTests()
        {
            _contractRepoMock = new Mock<IContractRepository>();
            _proposalGatewayMock = new Mock<IProposalGateway>();
            _useCase = new CreateContractUseCase(_contractRepoMock.Object, _proposalGatewayMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ProposalApproved_CreatesContract()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            var approvedProposal = new ProposalContractDto
            {
                Id = proposalId,
                CustomerName = "Test",
                InsuranceType = InsuranceType.Health,
                CoverageAmount = 100m,
                Status = ProposalStatus.Approved,
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow,
            };

            _proposalGatewayMock
                .Setup(g => g.GetProposalByIdAsync(proposalId))
                .ReturnsAsync(approvedProposal);

            _contractRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Contract>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.ExecuteAsync(proposalId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(proposalId, result.ProposalId);
            Assert.NotEqual(Guid.Empty, result.Id);
            _contractRepoMock.Verify(r => r.AddAsync(It.IsAny<Contract>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProposalNotApproved_ThrowsProposalNotApprovedException()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            var notApprovedProposal = new ProposalContractDto
            {
                Id = proposalId,
                CustomerName = "Test",
                InsuranceType = InsuranceType.Health,
                CoverageAmount = 100m,
                Status = ProposalStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _proposalGatewayMock
                .Setup(g => g.GetProposalByIdAsync(proposalId))
                .ReturnsAsync(notApprovedProposal);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ProposalNotApprovedException>(
                () => _useCase.ExecuteAsync(proposalId)
            );

            Assert.Equal(proposalId, ex.Id);
            _contractRepoMock.Verify(r => r.AddAsync(It.IsAny<Contract>()), Times.Never);
        }
    }
}
