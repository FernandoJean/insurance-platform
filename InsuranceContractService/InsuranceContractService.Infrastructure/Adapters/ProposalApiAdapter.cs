using InsuranceContractService.Domain.Dtos.Proposal;
using InsuranceContractService.Domain.Exceptions;
using InsuranceContractService.Domain.Ports;
using InsuranceContractService.Infrastructure.Adapters.Dtos;
using InsuranceContractService.Infrastructure.Adapters.Mappers;
using System.Net.Http.Json;

namespace InsuranceContractService.Infrastructure.Adapters
{
    public sealed class ProposalApiAdapter(HttpClient httpClient) : IProposalGateway
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ProposalContractDto> GetProposalByIdAsync(Guid proposalId)
        {
            var response = await _httpClient.GetFromJsonAsync<ProposalResponseDto>($"v1/proposals/{proposalId}") ?? throw new ProposalIdNotFoundException(proposalId);

            return ProposalMapper.ToContractDto(response);
        }
    }
}