using InsuranceContractService.Domain.Interfaces;
using InsuranceContractService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceContractService.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();

            return services;
        }
    }
}