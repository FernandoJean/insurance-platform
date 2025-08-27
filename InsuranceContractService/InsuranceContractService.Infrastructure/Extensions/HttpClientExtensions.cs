using InsuranceContractService.Domain.Ports;
using InsuranceContractService.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceContractService.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddProposalApiClient(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<IProposalGateway, ProposalApiAdapter>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            return services;
        }
    }
}