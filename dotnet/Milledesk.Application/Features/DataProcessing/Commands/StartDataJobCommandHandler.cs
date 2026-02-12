using MediatR;
using Milledesk.Domain.Entities;
using Milledesk.Domain.Interfaces;

namespace Milledesk.Application.Features.DataProcessing.Commands;

public class StartDataJobCommandHandler : IRequestHandler<StartDataJobCommand, string>
{
    private readonly IJobStore _jobStore;
    private readonly IJobQueue _jobQueue;

    public StartDataJobCommandHandler(
        IJobStore jobStore,
        IJobQueue jobQueue)
    {
        _jobStore = jobStore;
        _jobQueue = jobQueue;
    }

    public Task<string> Handle(
        StartDataJobCommand request,
        CancellationToken cancellationToken)
    {
        var job = new DataJob(request.ClientId);

        _jobStore.Add(job);
        _jobQueue.Enqueue(job);

        return Task.FromResult(job.JobId);
    }
}
