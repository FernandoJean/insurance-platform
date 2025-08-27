using Dapper;
using InsuranceQuoteService.Domain.Entities;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Interfaces;
using InsuranceQuoteService.Domain.Models;
using System.Data;

namespace InsuranceQuoteService.Infrastructure.Persistence
{
    public sealed class ProposalRepository(DatabaseConnection databaseConnection) : PostgresBase(databaseConnection), IProposalRepository
    {
        public async Task AddAsync(Proposal proposal)
        {
            const string query = @"INSERT INTO proposals (id, customer_name, insurance_type, coverage_amount, status, created_at) VALUES (@Id, @CustomerName, @InsuranceType, @CoverageAmount, @Status, @CreatedAt);";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", proposal.Id, DbType.Guid);
            parameters.Add("@CustomerName", proposal.CustomerName, DbType.String);
            parameters.Add("@InsuranceType", proposal.InsuranceType.ToString(), DbType.String);
            parameters.Add("@CoverageAmount", proposal.CoverageAmount, DbType.Decimal);
            parameters.Add("@Status", proposal.Status.ToString(), DbType.String);
            parameters.Add("@CreatedAt", proposal.CreatedAt, DbType.DateTime);

            await ExecuteAsync(query, parameters);
        }

        public async Task<Proposal?> GetByIdAsync(Guid id)
        {
            const string query = "SELECT id AS Id, customer_name AS CustomerName, insurance_type AS InsuranceType, coverage_amount AS CoverageAmount, status AS Status, created_at AS CreatedAt, updated_at AS UpdatedAt FROM proposals WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            return await QuerySingleOrDefaultAsync<Proposal?>(query, parameters);
        }

        public async Task<(long, IEnumerable<Proposal>)> ListAsync(Pagination pagination)
        {
            const string query = @"SELECT Count(*) FROM proposals; SELECT id AS Id, customer_name AS CustomerName, insurance_type AS InsuranceType, coverage_amount AS CoverageAmount, status AS Status, created_at AS CreatedAt, updated_at AS UpdatedAt FROM proposals ORDER BY created_at DESC LIMIT @PageSize OFFSET @Offset";

            var parameters = new DynamicParameters();
            parameters.Add("@Offset", pagination.PageSize * pagination.PageIndex);
            parameters.Add("@PageSize", pagination.PageSize);

            return await ExecuteMultiReaderAsync<Proposal>(query, parameters);
        }

        public async Task<int> UpdateStatusAsync(Guid id, ProposalStatus newStatus)
        {
            const string query = @"UPDATE proposals SET status = @Status, updated_at = @UpdatedAt WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);
            parameters.Add("@Status", newStatus.ToString(), DbType.String);
            parameters.Add("@UpdatedAt", DateTime.UtcNow, DbType.DateTime);

            return await ExecuteAsyncWithRowCount(query, parameters);
        }
    }
}