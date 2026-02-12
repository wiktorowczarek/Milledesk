using Microsoft.Extensions.Hosting;
using Milledesk.Application.Abstractions;
using Milledesk.Domain.Entities;
using Milledesk.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Check cache
            var cached = await _cacheService.GetAsync<string>(cacheKey, token);
            if (cached != null)
            {
                job.Complete(cached);
                return;
            }

            // Every 10th request fails
            var currentCount = _requestCounter.Increment();
            if (currentCount % 10 == 0)
            {
                job.Fail("Simulated error on every 10th request.");
                return;
            }

            // Simulate 60s processing
            await Task.Delay(TimeSpan.FromSeconds(60), token);

            var result = $"Generated data for client {job.ClientId} at {DateTime.UtcNow}";

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), token);

            job.Complete(result);
        }
    }
}
