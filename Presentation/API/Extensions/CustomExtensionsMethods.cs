using API.Filters;
using Application.Common.Interfaces;
using Common;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace API.Extensions
{

    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomOpenAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Base Project", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."+
                   "\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below."+
                    "\r\n\r\nExample: \"Bearer xxxxx\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });

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
