using InsuranceContractService.Domain.Entities;
using InsuranceContractService.Domain.Interfaces;

namespace InsuranceContractService.Infrastructure.Persistence
{
    public sealed class ContractRepository(DatabaseConnection databaseConnection) : PostgresBase(databaseConnection), IContractRepository
    {
        public async Task AddAsync(Contract contract)
        {
            const string query = @"INSERT INTO contracts (id, proposal_id, contracted_at) VALUES (@Id, @ProposalId, @ContractedAt);";

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("Id", contract.Id);
            parameters.Add("ProposalId", contract.ProposalId);
            parameters.Add("ContractedAt", contract.ContractedAt);

            await ExecuteAsync(query, parameters);
        }
    }
}