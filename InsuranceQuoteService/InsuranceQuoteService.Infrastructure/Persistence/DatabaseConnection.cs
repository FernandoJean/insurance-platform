using Npgsql;

namespace InsuranceQuoteService.Infrastructure.Persistence
{
    public sealed class DatabaseConnection(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}