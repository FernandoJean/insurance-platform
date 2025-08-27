using FluentMigrator.Runner;
using InsuranceQuoteService.Infrastructure.Persistence.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceQuoteService.Infrastructure.Extensions
{
    public static class MigrationsExtensions
    {
        public static IServiceCollection AddCustomMigrations(this IServiceCollection services, string connectionString)
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(CreateProposalsTable).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole()
                );

            return services;
        }
    }
}