using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common;
using Microsoft.Extensions.Configuration;

namespace Common
{
    public static class Configuration
    {
        public static IConfigurationRoot _configuration;

        public static void Build(string pathJsonFile)
        {
            var environment1 = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var environment2 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(pathJsonFile)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment1}.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment2}.json", optional: true, reloadOnChange: false)
                .AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public static IConfiguration GetConfiguration()
        {
            return _configuration;
        }
        public static List<RequestPerformanceBehaviourSetting> RequestPerformanceBehaviourSettings
        {
            get
            {
                var list = new List<RequestPerformanceBehaviourSetting>();
                try
                {
                    _configuration.GetSection("RequestPerformanceBehaviourSettings")?.Bind(list);
                }
                catch
                {

                }

                if (!list.Any())
                    list.Add(new RequestPerformanceBehaviourSetting() { Resource = "Default", ExecutionLimit = 1000 });

                return list;
            }
        }

        public static bool IsLocalhost => bool.Parse(_configuration.GetSection("AppSettings")["IsLocalhost"]);
        public static string ContextConnectionString => _configuration.GetConnectionString("ContextConnectionString");

        public static string JwtIssuer => _configuration.GetSection("Jwt")["Issuer"];
        public static string JwtAudience => _configuration.GetSection("Jwt")["Audience"];
        public static string JWtKey => _configuration.GetSection("Jwt")["Key"];
    }
}
