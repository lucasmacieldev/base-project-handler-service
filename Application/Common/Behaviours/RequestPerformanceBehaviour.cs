using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Application.Common.Behaviours
{

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private string[] _labels;
        private readonly Stopwatch _timer;
        private readonly ILogger<object> _logger;
        private readonly List<RequestPerformanceBehaviourSetting> _settings;

        public RequestPerformanceBehaviour(ILogger<object> logger)
        {
            _labels = new List<string>() { "name" }.ToArray();
            _timer = new Stopwatch();

            _settings = Configuration.RequestPerformanceBehaviourSettings;

            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var name = typeof(TRequest).Name;

            var limit = _settings.First(x => x.Resource == "Default").ExecutionLimit;

            var setting = _settings.FirstOrDefault(x => x.Resource == name);

            if (setting != null)
                limit = setting.ExecutionLimit;

            return response;
        }
    }
}
