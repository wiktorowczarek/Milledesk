using Milledesk.Domain.Entities;
using Milledesk.Domain.Interfaces;
using System.Threading.Channels;

namespace Milledesk.Infrastructure.Queue
{
    public class InMemoryJobQueue : IJobQueue
    {
        private readonly Channel<DataJob> _queue = Channel.CreateUnbounded<DataJob>();

        public void Enqueue(DataJob job)
        {
            _queue.Writer.TryWrite(job);
        }

        public async ValueTask<DataJob> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
