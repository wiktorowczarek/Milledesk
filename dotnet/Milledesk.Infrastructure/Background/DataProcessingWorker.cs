using Microsoft.Extensions.Hosting;
using Milledesk.Application.Abstractions;
using Milledesk.Domain.Entities;
using Milledesk.Domain.Interfaces;

namespace Milledesk.Infrastructure.Background
{
    public class DataProcessingWorker : BackgroundService
    {
        private readonly IJobQueue _jobQueue;
        private readonly IRequestCounter _requestCounter;
        private readonly ICacheService _cacheService;

        public DataProcessingWorker(
            IJobQueue jobQueue,
            IRequestCounter requestCounter,
            ICacheService cacheService)
        {
            _jobQueue = jobQueue;
            _requestCounter = requestCounter;
            _cacheService = cacheService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _jobQueue.DequeueAsync(stoppingToken);

                _ = ProcessJobAsync(job, stoppingToken);
            }
        }

        private async Task ProcessJobAsync(DataJob job, CancellationToken token)
        {
            var cacheKey = $"data-{job.ClientId}";

            var cached = await _cacheService.GetAsync<string>(cacheKey, token);
            if (cached != null)
            {
                job.Complete(cached);
                return;
            }

            var currentCount = _requestCounter.Increment();
            if (currentCount % 10 == 0)
            {
                job.Fail("Simulated error on every 10th request.");
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(60), token);

            var result = $"Generated data for client {job.ClientId} at {DateTime.UtcNow}";

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), token);

            job.Complete(result);
        }
    }
}
