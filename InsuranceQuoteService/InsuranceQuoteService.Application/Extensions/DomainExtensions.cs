using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceQuoteService.Application.Extensions
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateProposalUseCase, CreateProposalUseCase>();
            services.AddScoped<IGetProposalByIdUseCase, GetProposalByIdUseCase>();
            services.AddScoped<IListProposalsUseCase, ListProposalsUseCase>();
            services.AddScoped<IUpdateProposalsStatusUseCase, UpdateProposalsStatusUseCase>();

            return services;
        }
    }
}