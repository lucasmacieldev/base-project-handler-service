using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System.IO;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Configuration.Build(Directory.GetCurrentDirectory());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:6580");
                });
    }
}
