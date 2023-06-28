using API.Filters;
using Application.Common.Interfaces;
using Common;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace API.Extensions
{

    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomOpenAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Base Project", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddCustomFramework(this IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            )
            .AddJsonOptions(option =>
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            )
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<IPaginator>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddSingleton(option =>
            {
                return Configuration._configuration;
            });

            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("X-TraceId");
            });

            return services;
        }

        public static IServiceCollection AddCustomHealthChecks(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();

            //hcBuilder.AddPersistenceHealthCheck();

            return services;
        }

    }
}
