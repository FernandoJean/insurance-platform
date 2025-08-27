using FluentMigrator.Runner;
using InsuranceContractService.Infrastructure.Persistence.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceContractService.Infrastructure.Extensions
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
                    .ScanIn(typeof(CreateContractsTable).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole()
                );

            return services;
        }
    }
}