using Microsoft.AspNetCore.CookiePolicy;

namespace InsuranceQuoteService.Presentation.Extensions
{
    /// <summary>
    /// Extensões para configurar segurança da aplicação, incluindo políticas de cookies e HSTS.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Adiciona serviços de segurança personalizados, configurando políticas de cookies e HSTS.
        /// </summary>
        public static IServiceCollection AddCustomSecurity(this IServiceCollection services)
        {
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

            return services;
        }

        /// <summary>
        /// Adiciona o middleware de segurança personalizado na pipeline da aplicação, ativando HSTS e política de cookies.
        /// </summary>
        public static IApplicationBuilder UseCustomSecurity(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseHsts();

            return app;
        }
    }
}