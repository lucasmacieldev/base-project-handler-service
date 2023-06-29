using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.IO;
using System.Threading.Tasks;

namespace Mobby.Uber.Persistence.Migration
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Configuration.Build(Directory.GetCurrentDirectory());

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConfiguration(Configuration.GetConfiguration().GetSection("Logging"))
                .AddSimpleConsole();
            });
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .UseSqlServer(Configuration.ContextConnectionString);

            await new Context(optionsBuilder.Options).Database.MigrateAsync();
        }
    }
}
