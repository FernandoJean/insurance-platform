using Dapper;

namespace InsuranceContractService.Infrastructure.Persistence
{
    public class PostgresBase(DatabaseConnection databaseConnection)
    {
        private readonly DatabaseConnection _databaseConnection = databaseConnection;

        protected async Task<int> ExecuteAsync(string sql, DynamicParameters parameter)
        {
            try
            {
                await using var connection = _databaseConnection.GetConnection();

                return await connection.ExecuteAsync(sql, parameter);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString());
            }
        }
    }
}