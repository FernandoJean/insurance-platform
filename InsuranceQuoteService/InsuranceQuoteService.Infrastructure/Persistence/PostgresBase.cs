using Dapper;

namespace InsuranceQuoteService.Infrastructure.Persistence
{
    public class PostgresBase(DatabaseConnection databaseConnection)
    {
        private readonly DatabaseConnection _databaseConnection = databaseConnection;

        protected async Task<int> ExecuteAsync(string sql, DynamicParameters parameter, CancellationToken ctx)
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

        protected async Task<T> QuerySingleOrDefaultAsync<T>(string query, DynamicParameters parameter, CancellationToken ctx)
        {
            await using var connection = _databaseConnection.GetConnection();

            return await connection.QuerySingleOrDefaultAsync<T>(query, parameter) ?? default!;
        }

        protected async Task<(long, List<T>)> ExecuteMultiReaderAsync<T>(string query, DynamicParameters parameter, CancellationToken ctx)
        {
            await using var connection = _databaseConnection.GetConnection();

            var multi = await connection.QueryMultipleAsync(query, parameter);

            return (multi.ReadAsync<long>().Result.SingleOrDefault(), multi.ReadAsync<T>().Result.ToList());
        }

        protected async Task<int> ExecuteAsyncWithRowCount(string query, DynamicParameters parameter, CancellationToken ctx)
        {
            await using var connection = _databaseConnection.GetConnection();

            return await connection.ExecuteAsync(query, parameter);
        }
    }
}