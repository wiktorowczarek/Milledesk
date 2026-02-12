using Milledesk.Domain.Entities;

namespace Milledesk.Domain.Interfaces
{
    public interface IJobQueue
    {
        void Enqueue(DataJob job);
        ValueTask<DataJob> DequeueAsync(CancellationToken cancellationToken);
    }
}
