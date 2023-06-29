using MediatR;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;
using Common;

namespace Application.Common.Behaviours
{
    public class PaginationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<object> _logger;
        private readonly int _defaulgPagination;

        public PaginationBehaviour(ILogger<object> logger)
        {
            _defaulgPagination = int.Parse(Configuration.GetConfiguration()[ConfigurationIdentifiers.DefaultLimitToResponse]);
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IPaginator)
            {
                if ((request as IPaginator).Limit == 0)
                {
                    (request as IPaginator).Limit = _defaulgPagination;
                    (request as IPaginator).Offset = 0;
                }
            }

            var response = await next();

            return response;
        }
    }
}
