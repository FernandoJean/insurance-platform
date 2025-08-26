using InsuranceQuoteService.Domain.Interfaces;

namespace InsuranceQuoteService.Infrastructure.Persistence
{
    public sealed class QuoteRepository :IQuoteRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public QuoteRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
    }
}
