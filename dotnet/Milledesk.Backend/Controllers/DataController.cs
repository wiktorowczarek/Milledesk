using MediatR;
using Microsoft.AspNetCore.Mvc;
using Milledesk.Api.Extensions;
using Milledesk.Application.Features.DataProcessing.Commands;
using Milledesk.Application.Features.DataProcessing.Queries;
using Milledesk.Domain.Enums;

namespace Milledesk.Api.Controllers;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Starts data processing job
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> StartProcessing(CancellationToken cancellationToken)
    {
        var clientId = HttpContext.GetOrCreateClientId();

        var jobId = await _mediator.Send(
            new StartDataJobCommand(clientId),
            cancellationToken);

        return Accepted(new { jobId });
    }

    /// <summary>
    /// Gets job status
    /// </summary>
    [HttpGet("{jobId}")]
    public async Task<IActionResult> GetStatus(
        string jobId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDataJobQuery(jobId), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return result.Status switch
        {
            JobStatus.Processing => Accepted(new
            {
                status = "Processing"
            }),

            JobStatus.Completed => Ok(new
            {
                status = "Completed",
                data = result.Data
            }),

            JobStatus.Failed => StatusCode(500, new
            {
                status = "Failed",
                error = result.Data
            }),

            _ => BadRequest()
        };
    }
}
