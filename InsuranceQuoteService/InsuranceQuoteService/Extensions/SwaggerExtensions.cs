using Microsoft.OpenApi.Models;

namespace InsuranceQuoteService.Presentation.Extensions
{
    /// <summary>
    /// Extensões para configurar Swagger na aplicação, incluindo documentação e UI.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Adiciona serviços de Swagger personalizados à coleção de serviços, configurando documentação e UI.
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
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

            services.AddEndpointsApiExplorer();

            return services;
        }

        /// <summary>
        /// Adiciona o middleware de Swagger à pipeline da aplicação, habilitando a UI no ambiente de desenvolvimento.
        /// </summary>
        public static WebApplication UseCustomSwagger(this WebApplication app)
        {
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

            return app;
        }
    }
}