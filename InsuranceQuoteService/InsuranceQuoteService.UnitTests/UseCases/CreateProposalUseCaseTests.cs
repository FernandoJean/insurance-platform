using InsuranceQuoteService.Application.UseCases;
using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Interfaces;
using Moq;

namespace InsuranceQuoteService.UnitTests.UseCases
{
    public class CreateProposalUseCaseTests
    {
        private readonly Mock<IProposalRepository> _proposalRepositoryMock;
        private readonly CreateProposalUseCase _useCase;

        public CreateProposalUseCaseTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _useCase = new CreateProposalUseCase(_proposalRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidRequest_ReturnsProposalResponse()
        {
            // Arrange
            var request = new CreateProposalRequestDto
            {
                CustomerName = "Fernando",
                InsuranceType = InsuranceType.Life,
                CoverageAmount = 10000m
            };

            Proposal? capturedProposal = null;
            _proposalRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Proposal>()))
                .Callback<Proposal>(p => capturedProposal = p)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.ExecuteAsync(request);

            // Assert
            _proposalRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Proposal>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(request.CustomerName, result.CustomerName);
            Assert.Equal(request.CoverageAmount, result.CoverageAmount);
            Assert.Equal(request.InsuranceType, result.InsuranceType);
            Assert.Equal(ProposalStatus.Pending, result.Status);

            Assert.NotNull(capturedProposal);
            Assert.Equal(request.CustomerName, capturedProposal!.CustomerName);
            Assert.Equal(request.CoverageAmount, capturedProposal.CoverageAmount);
            Assert.Equal(request.InsuranceType, capturedProposal.InsuranceType);
            Assert.Equal(ProposalStatus.Pending, capturedProposal.Status);
        }
    }
}
