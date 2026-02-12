using Microsoft.Extensions.DependencyInjection;
using Milledesk.Application.Abstractions;
using Milledesk.Domain.Interfaces;
using Milledesk.Infrastructure.Background;
using Milledesk.Infrastructure.Caching;
using Milledesk.Infrastructure.Counters;
using Milledesk.Infrastructure.Queue;
using Milledesk.Infrastructure.Storage;

namespace Milledesk.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<IJobStore, InMemoryJobStore>();
            services.AddSingleton<IJobQueue, InMemoryJobQueue>();
            services.AddSingleton<IRequestCounter, RequestCounter>();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            services.AddHostedService<DataProcessingWorker>();

            return services;
        }
    }
}
