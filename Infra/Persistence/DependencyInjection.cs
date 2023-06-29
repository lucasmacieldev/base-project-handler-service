using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration.ContextConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            },
                ServiceLifetime.Scoped
            );

            services.AddScoped<IContext, Context>();

            return services;
        }

        public static IHealthChecksBuilder AddPersistenceHealthCheck(this IHealthChecksBuilder hcBuilder)
        {
            hcBuilder
                .AddSqlServer(
                    Configuration.ContextConnectionString,
                    name: "Database: basecrud",
                    tags: new string[] { "basecrud" });

            return hcBuilder;
        }
    }
}
