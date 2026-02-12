using Milledesk.Domain.Entities;

namespace Milledesk.Domain.Interfaces
{
    public interface IJobStore
    {
        void Add(DataJob job);
        DataJob? Get(string jobId);
    }
}
