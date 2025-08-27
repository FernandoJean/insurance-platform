using FluentMigrator.Runner;
using InsuranceQuoteService.Application.Extensions;
using InsuranceQuoteService.Infrastructure.Extensions;
using InsuranceQuoteService.Infrastructure.Persistence;
using InsuranceQuoteService.Infrastructure.Persistence.Exceptions;
using InsuranceQuoteService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddCustomControllers();

services.AddCustomSecurity();

var connectionString = ((IConfiguration)builder.Configuration)["ConnectionStrings:InsuranceQuoteDb"] ?? throw new InsuranceQuoteDbConnectionStringException();
services.AddSingleton(sp => new DatabaseConnection(connectionString));

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