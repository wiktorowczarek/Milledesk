using MediatR;
using Milledesk.Application.Features.DataProcessing.DTOs;
using Milledesk.Application.Features.DataProcessing.Queries;
using Milledesk.Domain.Interfaces;

namespace Milledesk.Application.Features.DataProcessing.Queries;

public class GetDataJobQueryHandler : IRequestHandler<GetDataJobQuery, DataJobResponseDto?>
{
    private readonly IJobStore _jobStore;

    public GetDataJobQueryHandler(IJobStore jobStore)
    {
        _jobStore = jobStore;
    }

    public Task<DataJobResponseDto?> Handle(
        GetDataJobQuery request,
        CancellationToken cancellationToken)
    {
        var job = _jobStore.Get(request.JobId);

        if (job == null)
            return Task.FromResult<DataJobResponseDto?>(null);

        var response = new DataJobResponseDto
        {
            JobId = job.JobId,
            Status = job.Status,
            Data = job.Result
        };

        return Task.FromResult<DataJobResponseDto?>(response);
    }
}
