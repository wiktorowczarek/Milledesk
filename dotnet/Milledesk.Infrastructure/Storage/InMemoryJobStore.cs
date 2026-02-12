using Milledesk.Domain.Entities;
using Milledesk.Domain.Interfaces;
using System.Collections.Concurrent;

namespace Milledesk.Infrastructure.Storage
{
    public class InMemoryJobStore : IJobStore
    {
        private readonly ConcurrentDictionary<string, DataJob> _jobs = new();

        public void Add(DataJob job)
        {
            _jobs[job.JobId] = job;
        }

        public DataJob? Get(string jobId)
        {
            _jobs.TryGetValue(jobId, out var job);
            return job;
        }
    }
}
