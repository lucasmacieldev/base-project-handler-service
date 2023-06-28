using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //_ = services.AddMediatR(Assembly.GetExecutingAssembly());
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(PaginationBehaviour<,>));
            _ = services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //_ = services.AddAESProvider(Configuration._configuration);
            //_ = services.AddArtemis(Configuration._configuration);
            //_ = services.RegisterOptions<CerberusOptions>();
            //_ = services.AddHttpClient<ICerberusService, CerberusService>();

            // MSHService Integrations
            //_ = services.RegisterOptions<UberProxyApiOptions>();
            //_ = services.AddHttpClient<IUberProxyApiService, UberProxyApiService>();
            //_ = services.AddMobbyOrderApi(Configuration.MobbyOrderApiUrl);

            return services;
        }

        //public static IServiceCollection AddMediaRTypes(this IServiceCollection services, List<Type> types)
        //{
        //    types.ForEach(type => _ = services.AddMediatR(type));
        //    return services;
        //}

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
