using API.Extensions;
using API.Middlewares;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Prometheus;

namespace API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomFramework();
            //services.AddTracer();
            services.AddCustomOpenAPI();
            services.AddCustomHealthChecks();
            services.AddApplication();
            services.AddHealthChecks();
            services.AddPersistence();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobby Uber v1");
                c.RoutePrefix = "docs";
            });

            //app.UseMetricServer().UseHttpMetrics();
            app.UseHealthChecksPrometheusExporter("/healthmetrics");

            app.UseRouting();
            app.UseCustomExceptionHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to /health to see the health status.");
                });
                endpoints.MapControllers();
            });
        }
    }
}
