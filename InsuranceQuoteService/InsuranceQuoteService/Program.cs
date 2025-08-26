using FluentMigrator.Runner;
using InsuranceQuoteService.Domain.Interfaces;
using InsuranceQuoteService.Infrastructure.Persistence;
using InsuranceQuoteService.Infrastructure.Persistence.Exceptions;
using InsuranceQuoteService.Infrastructure.Persistence.Migrations;
using InsuranceQuoteService.Presentation.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

// Cache
//services.AddMemoryCache();

services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

services.AddEndpointsApiExplorer();

services.AddRouting(options => options.LowercaseUrls = true);

services
    .AddSwaggerGen(swagger =>
    {
        swagger.EnableAnnotations();
        swagger.OrderActionsBy(api => api.RelativePath);
        swagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Insurance Quote Service API",
            Description = "API para gerenciamento de cotações de seguros.",
            Version = "v1"
        });

        swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "InsuranceQuoteService.API.xml"));
    });

services.Configure<CookiePolicyOptions>(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

var connectionString = configuration["ConnectionStrings:InsuranceQuoteDb"] ?? throw new InsuranceQuoteDbConnectionStringException();
services.AddSingleton(sp => new DatabaseConnection(connectionString));

services.AddScoped<IQuoteRepository, QuoteRepository>();

services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres() 
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(CreateQuotesTable).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

services.AddMvc(options => { options.Filters.Add<ApiResponseExceptionFilterAttribute>(); });

var app = builder.Build();

app.UseCookiePolicy();
app.UseHsts();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceQuoteService API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
