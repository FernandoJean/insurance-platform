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
        public async Task AddAsync(Proposal proposal, CancellationToken ctx)
        {
            const string query = @"INSERT INTO proposals (id, customer_name, insurance_type, coverage_amount, status, created_at) VALUES (@Id, @CustomerName, @InsuranceType, @CoverageAmount, @Status, @CreatedAt);";

            var parameter = new DynamicParameters();
            parameter.Add("@Id", proposal.Id, DbType.Guid);
            parameter.Add("@CustomerName", proposal.CustomerName, DbType.String);
            parameter.Add("@InsuranceType", proposal.InsuranceType.ToString(), DbType.String);
            parameter.Add("@CoverageAmount", proposal.CoverageAmount, DbType.Decimal);
            parameter.Add("@Status", proposal.Status.ToString(), DbType.String);
            parameter.Add("@CreatedAt", proposal.CreatedAt, DbType.DateTime);

            await ExecuteAsync(query, parameter, ctx);
        }

        public async Task<Proposal?> GetByIdAsync(Guid id, CancellationToken ctx)
        {
            const string query = "SELECT id AS Id, customer_name AS CustomerName, insurance_type AS InsuranceType, coverage_amount AS CoverageAmount, status AS Status, created_at AS CreatedAt, updated_at AS UpdatedAt FROM proposals WHERE id = @Id";

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Guid);

            return await QuerySingleOrDefaultAsync<Proposal?>(query, parameter, ctx);
        }

        public async Task<(long, IEnumerable<Proposal>)> ListAsync(Pagination pagination, CancellationToken ctx)
        {
            const string query = @"SELECT Count(*) FROM proposals; SELECT id AS Id, customer_name AS CustomerName, insurance_type AS InsuranceType, coverage_amount AS CoverageAmount, status AS Status, created_at AS CreatedAt, updated_at AS UpdatedAt FROM proposals ORDER BY created_at DESC LIMIT @PageSize OFFSET @Offset";

            var parameter = new DynamicParameters();
            parameter.Add("@Offset", pagination.PageSize * pagination.PageIndex);
            parameter.Add("@PageSize", pagination.PageSize);

            return await ExecuteMultiReaderAsync<Proposal>(query, parameter, ctx);
        }

        public async Task<int> UpdateStatusAsync(Guid id, ProposalStatus newStatus, CancellationToken ctx)
        {
            const string query = @"UPDATE proposals SET status = @Status, updated_at = @UpdatedAt WHERE id = @Id";

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Guid);
            parameter.Add("@Status", newStatus.ToString(), DbType.String);
            parameter.Add("@UpdatedAt", DateTime.UtcNow, DbType.DateTime);

            return await ExecuteAsyncWithRowCount(query, parameter, ctx);
        }
    }
}