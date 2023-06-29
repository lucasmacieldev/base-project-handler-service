using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(PaginationBehaviour<,>));
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddMediaRTypes(this IServiceCollection services, List<Type> types)
        {
            types.ForEach(type => _ = services.AddMediatR(type));
            return services;
        }

        //public static IServiceCollection AddDefaultWorkersBrokersServices(this IServiceCollection services)
        //{
        //    _ = services.RegisterOptions<AkkaOptions>()
        //                .AddArtemis(Configuration._configuration)
        //                .AddSingleton(config => ActorSystem.Create(Configuration.GetConfiguration().GetSection("AkkaOptions")["ActorName"]))
        //                .AddSingleton<IMaterializer>(config => config.GetRequiredService<ActorSystem>().Materializer());

        //    return services;
        //}
    }
}
