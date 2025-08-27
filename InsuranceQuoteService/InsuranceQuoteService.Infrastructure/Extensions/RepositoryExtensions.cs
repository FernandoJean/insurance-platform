using InsuranceQuoteService.Domain.Interfaces;
using InsuranceQuoteService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceQuoteService.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProposalRepository, ProposalRepository>();

            return services;
        }
    }
}