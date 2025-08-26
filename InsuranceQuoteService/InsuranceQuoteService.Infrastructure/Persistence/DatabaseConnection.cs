using InsuranceQuoteService.Domain.Interfaces;
using Npgsql;

namespace InsuranceQuoteService.Infrastructure.Persistence
{
    public sealed class DatabaseConnection : IQuoteRepository
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return connection;
        } 
    }
}
