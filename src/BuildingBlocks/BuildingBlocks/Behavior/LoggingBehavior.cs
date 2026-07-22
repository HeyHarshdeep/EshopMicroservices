using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BuildingBlocks.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Handle request={Request} - Response= {Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = Stopwatch.StartNew();

            var response = await next();

            timer.Stop();

            var timetaken = timer.Elapsed;
            if (timetaken.TotalSeconds > 3)
            {
                _logger.LogWarning("[Performance] The Request {Request} took {ElapsedSeconds} seconds", typeof(TRequest).Name, timetaken.TotalSeconds);
            }

            _logger.LogInformation("[End] Handled Request {Request} With {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
