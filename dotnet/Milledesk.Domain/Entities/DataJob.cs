using Milledesk.Domain.Enums;

namespace Milledesk.Domain.Entities
{
    public class DataJob
    {
        public string JobId { get; private set; }
        public string ClientId { get; private set; }
        public JobStatus Status { get; private set; }
        public string? Result { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public DataJob(string clientId)
        {
            JobId = Guid.NewGuid().ToString();
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            Status = JobStatus.Processing;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void Complete(string result)
        {
            if (Status != JobStatus.Processing)
                throw new InvalidOperationException("Only processing jobs can be completed.");

            Result = result;
            Status = JobStatus.Completed;
        }

        public void Fail(string errorMessage)
        {
            if (Status != JobStatus.Processing)
                throw new InvalidOperationException("Only processing jobs can fail.");

            Result = errorMessage;
            Status = JobStatus.Failed;
        }
    }
}
