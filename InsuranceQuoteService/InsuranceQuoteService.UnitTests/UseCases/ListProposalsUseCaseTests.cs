using InsuranceQuoteService.Application.UseCases;
using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Interfaces;
using InsuranceQuoteService.Domain.Models;
using Moq;

namespace InsuranceQuoteService.UnitTests.UseCases
{
    public class ListProposalsUseCaseTests
    {
        private readonly Mock<IProposalRepository> _proposalRepositoryMock;
        private readonly ListProposalsUseCase _useCase;

        public ListProposalsUseCaseTests()
        {
            _proposalRepositoryMock = new Mock<IProposalRepository>();
            _useCase = new ListProposalsUseCase(_proposalRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnsData_ShouldReturnCorrectPageModel()
        {
            // Arrange
            var pagination = new Pagination(0, 10);
            var proposals = new List<Proposal>
            {
                new() {
                    Id = Guid.NewGuid(),
                    CustomerName = "Fernando",
                    InsuranceType = InsuranceType.Health,
                    CoverageAmount = 5000,
                    Status = ProposalStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _proposalRepositoryMock
                .Setup(x => x.ListAsync(pagination))
                .ReturnsAsync((1, proposals));

            // Act
            var result = await _useCase.ExecuteAsync(pagination);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Total is int t ? t : int.Parse(result.Total.ToString()!));
            Assert.Single(result.Result);

            var dto = result.Result.First();
            Assert.Equal("Fernando", dto.CustomerName);
            Assert.Equal(InsuranceType.Health, dto.InsuranceType);
            Assert.Equal(5000, dto.CoverageAmount);
            Assert.Equal(ProposalStatus.Pending, dto.Status);

            _proposalRepositoryMock.Verify(x => x.ListAsync(pagination), Times.Once);
        }
    }
}
