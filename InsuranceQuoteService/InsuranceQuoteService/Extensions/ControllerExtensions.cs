using InsuranceQuoteService.Presentation.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceQuoteService.Presentation.Extensions
{
    /// <summary>
    /// Extensões para configurar controllers personalizados na aplicação.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Adiciona controllers personalizados ao IServiceCollection, configurando filtros e opções JSON.
        /// </summary>
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ApiResponseExceptionFilterAttribute>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }
    }
}