using FluentMigrator.Runner;
using InsuranceContractService.Application.Extensions;
using InsuranceContractService.Infrastructure.Extensions;
using InsuranceContractService.Infrastructure.Persistence;
using InsuranceContractService.Infrastructure.Persistence.Exceptions;
using InsuranceContractService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddCustomControllers();

services.AddCustomSecurity();

var connectionString = ((IConfiguration)builder.Configuration)["ConnectionStrings:InsuranceContractDb"] ?? throw new InsuranceContractDbConnectionStringException();
services.AddSingleton(sp => new DatabaseConnection(connectionString));

var proposalApiUrl = builder.Configuration["ProposalApi:BaseUrl"] ?? throw new ProposalApiBaseUrlNotConfiguredException();
services.AddProposalApiClient(proposalApiUrl);

services.AddRepositories();
services.AddCustomMigrations(connectionString);

services.AddCustomSwagger();

services.AddDomainUseCases();

var app = builder.Build();

app.UseCustomSecurity();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();