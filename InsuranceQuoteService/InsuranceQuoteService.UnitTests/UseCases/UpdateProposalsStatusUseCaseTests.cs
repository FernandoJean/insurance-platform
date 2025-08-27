using InsuranceQuoteService.Application.UseCases;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Exceptions;
using InsuranceQuoteService.Domain.Interfaces;
using Moq;

namespace InsuranceQuoteService.UnitTests.UseCases
{
    public class UpdateProposalsStatusUseCaseTests
    {
        private readonly Mock<IProposalRepository> _proposalRepositoryMock;
        private readonly UpdateProposalsStatusUseCase _useCase;

        public UpdateProposalsStatusUseCaseTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _useCase = new UpdateProposalsStatusUseCase(_proposalRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryUpdates_ReturnsSuccessfully()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            var newStatus = ProposalStatus.Approved;
            _proposalRepositoryMock
                .Setup(x => x.UpdateStatusAsync(proposalId, newStatus))
                .ReturnsAsync(1);

            // Act
            await _useCase.ExecuteAsync(proposalId, newStatus);

            // Assert
            _proposalRepositoryMock.Verify(x => x.UpdateStatusAsync(proposalId, newStatus), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WhenProposalIdDoesNotExist_ThrowsProposalIdNotFoundException()
        {
            // Arrange
            var proposalId = Guid.NewGuid();
            var newStatus = ProposalStatus.Approved;
            _proposalRepositoryMock
                .Setup(x => x.UpdateStatusAsync(proposalId, newStatus))
                .ReturnsAsync(0); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ProposalIdNotFoundException>(
                () => _useCase.ExecuteAsync(proposalId, newStatus)
            );

            Assert.Equal(proposalId, exception.Id);
            _proposalRepositoryMock.Verify(x => x.UpdateStatusAsync(proposalId, newStatus), Times.Once);
        }
    }
}
