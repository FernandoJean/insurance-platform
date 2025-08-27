using InsuranceContractService.Application.Interfaces;
using InsuranceContractService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceContractService.Application.Extensions
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateContractUseCase, CreateContractUseCase>();

            return services;
        }
    }
}